using Modelo.Domain.Dto;

namespace Modelo.Domain.Interfaces
{
    public interface ITokenService
    {
        public Task<ResultLoginDto> LoginAsync(string email, string password);
    }
}
