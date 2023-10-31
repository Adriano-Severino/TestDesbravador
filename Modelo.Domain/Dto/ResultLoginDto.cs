namespace Modelo.Domain.Dto
{
    public class ResultLoginDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
