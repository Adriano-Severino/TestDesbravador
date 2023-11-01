using Modelo.Domain.Enums;

namespace Modelo.Domain.Entities
{
    public class Employees : BaseEntity
    {
        public Employees()
        {
            Role = RoleEnum.User;
        }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public Guid? ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
