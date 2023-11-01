using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Application.Models;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Infra.CrossCutting.Models;
using Modelo.Service.Validators;

namespace Modelo.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProjectController : ControllerBase
    {
        private IBaseService<Project> _baseUserService;
        
        public ProjectController(IBaseService<Project> baseUserService)
        {
            _baseUserService = baseUserService;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateProjectModel createProject)
        {
            if (createProject == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseUserService.Add<CreateProjectModel, ProjectModel, ProjectValidator>(createProject));
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProjectModel updateProject)
        {
            if (updateProject == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseUserService.Update<UpdateProjectModel, ProjectModel, ProjectValidator>(updateProject));
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
            return await ExecuteAsync(async () => await _baseUserService.Get<ProjectModel>());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, User")]
        public async Task<IActionResult> Get(Guid id)
        {
            return await ExecuteAsync(async () => await _baseUserService.GetById<ProjectModel>(id));
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
