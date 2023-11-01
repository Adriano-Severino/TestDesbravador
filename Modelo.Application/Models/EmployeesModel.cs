using Modelo.Domain.Enums;

namespace Modelo.Application.Models
{
    public class EmployeesModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
