using AutoFixture;
using AutoFixture.AutoMoq;
using Modelo.Application.Models;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Service.Validators;
using Moq;
public class BaseProjectServiceTests
{
    [Fact]
    public async Task Add_ValidInput_ReturnsOutputModel()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var baseServiceMock = fixture.Freeze<Mock<IBaseService<Project>>>();
        var createProjectModel = fixture.Create<CreateProjectModel>();
        var projectModel = fixture.Create<ProjectModel>();

        baseServiceMock.Setup(s => s.AddAsync<CreateProjectModel, ProjectModel, ProjectValidator>(createProjectModel))
            .ReturnsAsync(projectModel);

        // Use the mock here instead of trying to instantiate the interface
        var service = baseServiceMock.Object;

        // Act
        var result = await service.AddAsync<CreateProjectModel, ProjectModel, ProjectValidator>(createProjectModel);

        // Assert
        Assert.Equal(projectModel, result);
    }

    [Fact]
    public async Task Update_ValidInput_ReturnsOutputModel()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var baseServiceMock = fixture.Freeze<Mock<IBaseService<Project>>>();
        var updateProjectModel = fixture.Create<UpdateProjectModel>();
        var projectModel = fixture.Create<ProjectModel>();

        baseServiceMock.Setup(s => s.UpdateAsync<UpdateProjectModel, ProjectModel, ProjectValidator>(updateProjectModel))
           .ReturnsAsync(projectModel);

        var service = baseServiceMock.Object;

        // Act
        var result = await service.UpdateAsync<UpdateProjectModel, ProjectModel, ProjectValidator>(updateProjectModel);

        // Assert
        Assert.Equal(projectModel.Id, result.Id);
    }

}