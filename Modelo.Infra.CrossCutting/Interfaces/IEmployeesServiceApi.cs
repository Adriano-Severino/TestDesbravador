using Modelo.Domain.Entities;

namespace Modelo.Infra.CrossCutting.Interfaces
{
    public interface IEmployeesServiceApi
    {
        public Task<EmployeesModels> GetEmployees();
    }
}
