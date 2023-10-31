using Microsoft.IdentityModel.Tokens;
using Modelo.Domain.Dto;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Modelo.Infra.CrossCutting.Utils.Key;

namespace Modelo.Service.Services
{
    public class TokenService : ITokenService
    {
        private IBaseService<User> _baseUserService;
        public TokenService(IBaseService<User> baseService)
        {
            _baseUserService = baseService;
        }

        public async Task<ResultLoginDto> LoginAsync(string email, string password)
        {
            var userDomain = _baseUserService.GetByEmail<User>(email);
            var resultLoginDto = new ResultLoginDto();
            if (userDomain == null)
            {
                resultLoginDto.Success = false;
                resultLoginDto.Message = "Erro ao tentar fazer login no sistema!";
                resultLoginDto.Token = null;
                return resultLoginDto;
            }

            if (userDomain.Password != userDomain.Password || userDomain.Name != userDomain.Name)
            {
                resultLoginDto.Success = false;
                resultLoginDto.Message = "Erro ao tentar fazer login no sistema!";
                resultLoginDto.Token = null;
                return resultLoginDto;
            }
            var token = await GetTokenAsync(userDomain);
            resultLoginDto.Success = true;
            resultLoginDto.Message = "Logim feito com sucesso!";
            resultLoginDto.Token = token;
            return resultLoginDto;
        }
        private async Task<string> GetTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("Role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}

