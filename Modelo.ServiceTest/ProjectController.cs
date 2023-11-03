using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Modelo.Application.Controllers;
using Modelo.Application.Models;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Service.Validators;
using Moq;

public class ProjectControllerTests
{
    [Fact]
    public async Task Create_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var baseServiceMock = fixture.Freeze<Mock<IBaseService<Project>>>();
        var createProjectModel = fixture.Create<CreateProjectModel>();
        var projectModel = fixture.Create<ProjectModel>();

        baseServiceMock.Setup(s => s.AddAsync<CreateProjectModel, ProjectModel, ProjectValidator>(createProjectModel))
            .ReturnsAsync(projectModel);

        var controller = new ProjectController(baseServiceMock.Object);

        // Act
        var result = await controller.CreateAsync(createProjectModel);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var baseServiceMock = fixture.Freeze<Mock<IBaseService<Project>>>();
        var updateProjectModel = fixture.Create<UpdateProjectModel>();
        var projectModel = fixture.Create<ProjectModel>();

        baseServiceMock.Setup(s => s.UpdateAsync<UpdateProjectModel, ProjectModel, ProjectValidator>(updateProjectModel))
            .ReturnsAsync(projectModel);

        var controller = new ProjectController(baseServiceMock.Object);

        // Act
        var result = await controller.UpdateAsync(updateProjectModel);

        // Assert
        Assert.IsType<OkObjectResult>(result);

    }

    [Fact]
    public async Task Get_ReturnsOkResult()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var baseServiceMock = fixture.Freeze<Mock<IBaseService<Project>>>();
        var project = fixture.Create<List<Project>>();
        var projectModel = fixture.Create<ProjectModel>();

        baseServiceMock.Setup(s => s.GetAsync<Project>())
               .ReturnsAsync(project);

        var controller = new ProjectController(baseServiceMock.Object);

        // Act
        var result = await controller.GetAsync();

        // Assert
        Assert.IsType<OkObjectResult>(result);

    }

    [Fact]
    public async Task Get_ValidInput_ReturnsOkObjectResult()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var baseProjectServiceMock = fixture.Freeze<Mock<IBaseService<Project>>>();
        var id = fixture.Create<Guid>();
        var expectedProjectModel = fixture.Create<ProjectModel>();

        baseProjectServiceMock.Setup(s => s.GetByIdAsync<ProjectModel>(id)).ReturnsAsync(expectedProjectModel);

        var controller = new ProjectController(baseProjectServiceMock.Object);

        // Act
        var result = await controller.GetAsync(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ProjectModel>(okResult.Value);
        Assert.Equal(expectedProjectModel, returnValue);
    }


}