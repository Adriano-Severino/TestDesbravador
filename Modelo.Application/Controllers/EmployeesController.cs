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
        public async Task<ResultLoginDto> LoginAsync([FromBody] LoginEmployeesModel user)
        {
            return await _tokenService.LoginAsync(user.Email, user.Password);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEmployeesModel createEmployees)
        {
            if (createEmployees == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseUserService.AddAsync<CreateEmployeesModel, EmployeesModel, EmployeesValidator>(createEmployees));
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        [Route("vinculo-projeto")]
        public async Task<IActionResult> VinculoProject([FromBody] UpdateEmployeesModel updateEmployees)
        {
            if (updateEmployees == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseUserService.AddAsync<UpdateEmployeesModel, EmployeesModel, EmployeesValidator>(updateEmployees));
        }


        [HttpGet]
        [Authorize(Policy = "Admin")]
        [Route("get-employees-api")]
        public async Task<IActionResult> CreateEmployeesByApi()
        {
            return await ExecuteAsync(async () => await _serviceEmployees.CreateEmployeesByApiAsync());
        }


        [HttpPut]
        [Authorize(Policy = "Admin")]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEmployeesModel updateEmployees)
        {
            if (updateEmployees == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseUserService.UpdateAsync<UpdateEmployeesModel, EmployeesModel, EmployeesValidator>(updateEmployees));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseUserService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }


        [HttpGet]
        [Route("get")]
        [Authorize(Policy = "Admin, User")]
        public async Task<IActionResult> GetAsync()
        {
            return await ExecuteAsync(async () => await _baseUserService.GetAsync<EmployeesModel>());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, User")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return await ExecuteAsync(async () => await _baseUserService.GetByIdAsync<EmployeesModel>(id));
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
