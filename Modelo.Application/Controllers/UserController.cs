using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Application.Models;
using Modelo.Domain.Dto;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Service.Validators;

namespace Modelo.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IBaseService<User> _baseUserService;
        private ITokenService _tokenService;

        public UserController(IBaseService<User> baseUserService, ITokenService tokenService)
        {
            _baseUserService = baseUserService;
            _tokenService = tokenService;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ResultLoginDto> Login([FromBody] LoginUserModel user)
        {
            return await _tokenService.LoginAsync(user.Email, user.Password);

        }
        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("create")]
        public IActionResult Create([FromBody] CreateUserModel user)
        {
            if (user == null)
                return NotFound();

            return Execute(() => _baseUserService.Add<CreateUserModel, UserModel, UserValidator>(user));
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        [Route("update")]
        public IActionResult Update([FromBody] UpdateUserModel user)
        {
            if (user == null)
                return NotFound();

            return Execute(() => _baseUserService.Update<UpdateUserModel, UserModel, UserValidator>(user));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(Guid id)
        {
            Execute(() =>
            {
                _baseUserService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }


        [HttpGet]
        [Route("get")]
        [Authorize(Policy = "Admin, User")]
        public IActionResult Get()
        {
            return Execute(() => _baseUserService.Get<UserModel>());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, User")]
        public IActionResult Get(Guid id)
        {
            return Execute(() => _baseUserService.GetById<UserModel>(id));
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
