using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Application.Models;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Service.Validators;

namespace Modelo.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IBaseService<Project> _baseProjectService;
        public ProjectController(IBaseService<Project> baseProjectService)
        {
            _baseProjectService = baseProjectService;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProjectModel createProject)
        {
            if (createProject == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseProjectService.AddAsync<CreateProjectModel, ProjectModel, ProjectValidator>(createProject));
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProjectModel updateProject)
        {
            if (updateProject == null)
                return NotFound();

            return await ExecuteAsync(async () => await _baseProjectService.UpdateAsync<UpdateProjectModel, ProjectModel, ProjectValidator>(updateProject));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseProjectService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }


        [HttpGet]
        [Route("get")]
        [Authorize(Policy = "Admin, User")]
        public async Task<IActionResult> GetAsync()
        {
            return await ExecuteAsync(async () => await _baseProjectService.GetAsync<Project>());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, User")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return await ExecuteAsync(async () => await _baseProjectService.GetByIdAsync<ProjectModel>(id));
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
