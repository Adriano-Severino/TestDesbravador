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
    [AllowAnonymous]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IBaseService<Employees> _baseUserService;
        private ITokenService _tokenService;
        private IServiceEmployees _serviceEmployees;


        public EmployeesController(IBaseService<Employees> baseUserService, ITokenService tokenService, IServiceEmployees serviceEmployees)
        {
            _baseUserService = baseUserService;
            _tokenService = tokenService;
            _serviceEmployees = serviceEmployees;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ResultLoginDto> Login([FromBody] LoginEmployeesModel user)
        {
            return await _tokenService.LoginAsync(user.Email, user.Password);

        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeesModel createEmployees)
        {
            if (createEmployees == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseUserService.Add<CreateEmployeesModel, EmployeesModel, EmployeesValidator>(createEmployees));
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        [Route("vinculo-projeto")]
        public async Task<IActionResult> VinculoProject([FromBody] UpdateEmployeesModel updateEmployees)
        {
            if (updateEmployees == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseUserService.Add<UpdateEmployeesModel, EmployeesModel, EmployeesValidator>(updateEmployees));
        }


        [HttpGet]
        [Authorize(Policy = "Admin")]
        [Route("get-employees-api")]
        public async Task<IActionResult> CreateEmployeesByApi()
        {
            return await ExecuteAsync(async () => await _serviceEmployees.CreateEmployeesByApi());
        }


        [HttpPut]
        [Authorize(Policy = "Admin")]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeesModel updateEmployees)
        {
            if (updateEmployees == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseUserService.Update<UpdateEmployeesModel, EmployeesModel, EmployeesValidator>(updateEmployees));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseUserService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }


        [HttpGet]
        [Route("get")]
        [Authorize(Policy = "Admin, User")]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAsync(async () => await _baseUserService.Get<EmployeesModel>());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, User")]
        public async Task<IActionResult> Get(Guid id)
        {
            return await ExecuteAsync(async () => await _baseUserService.GetById<EmployeesModel>(id));
        }

        private async Task<IActionResult> ExecuteAsync(Func<Task<object>> func)
        {
            try
            {
                var result = await func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
