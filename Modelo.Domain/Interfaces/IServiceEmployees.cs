using Modelo.Domain.Dto;

namespace Modelo.Domain.Interfaces
{
    public interface IServiceEmployees
    {
        public Task<List<CreateEmployeesDto>> CreateEmployeesByApiAsync();
    }
}