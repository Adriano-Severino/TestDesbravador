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
        private IBaseService<Employees> _baseUserService;
        public TokenService(IBaseService<Employees> baseService)
        {
            _baseUserService = baseService;
        }

        public async Task<ResultLoginDto> LoginAsync(string email, string password)
        {
            var userDomain = await _baseUserService.GetByEmailAsync<Employees>(email);
            var resultLoginDto = new ResultLoginDto();
            if (userDomain == null)
            {
                resultLoginDto.Success = false;
                resultLoginDto.Message = "Erro ao tentar fazer login no sistema!";
                resultLoginDto.Token = null;
                return resultLoginDto;
            }

            if (userDomain.Password != userDomain.Password || userDomain.Nome != userDomain.Nome)
            {
                resultLoginDto.Success = false;
                resultLoginDto.Message = "Erro ao tentar fazer login no sistema!";
                resultLoginDto.Token = null;
                return resultLoginDto;
            }
            var token = GetTokenAsync(userDomain);
            resultLoginDto.Success = true;
            resultLoginDto.Message = "Logim feito com sucesso!";
            resultLoginDto.Token = token;
            return resultLoginDto;
        }
        private string GetTokenAsync(Employees employees)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, employees.Id.ToString()),
                    new Claim(ClaimTypes.Name, employees.Nome),
                    new Claim("Role", employees.Role.ToString())
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

