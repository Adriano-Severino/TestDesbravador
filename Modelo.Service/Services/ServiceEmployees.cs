using Modelo.Domain.Dto;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Infra.CrossCutting.Interfaces;
using Modelo.Service.Validators;

namespace Modelo.Service.Services
{
    public class ServiceEmployees : IServiceEmployees
    {
        private IBaseService<Employees> _baseUserService;
        private IEmployeesServiceApi _employeesService;
        public ServiceEmployees(IBaseService<Employees> baseUserService, IEmployeesServiceApi employeesService)
        {
            _baseUserService = baseUserService;
            _employeesService = employeesService;
        }
        public async Task<List<CreateEmployeesDto>> CreateEmployeesByApi()
        {
            var employeesList = await _employeesService.GetEmployees();
            var result = new List<CreateEmployeesDto>();
            foreach (var item in employeesList.Results)
            {
                var createEmployeesDto = new CreateEmployeesDto();
                createEmployeesDto.Nome = item.Name.First;
                createEmployeesDto.Sobrenome = item.Name.Last;
                createEmployeesDto.Email = item.Email;
                createEmployeesDto.Password = item.Login.Password;
                await _baseUserService.Add<CreateEmployeesDto, Employees, EmployeesValidator>(createEmployeesDto);
                result.Add(createEmployeesDto);

            }
            return result;
        }

    }
}
