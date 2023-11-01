namespace Modelo.Domain.Entities
{
    public class Employees : BaseEntity
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = new string("User");
    }
}
