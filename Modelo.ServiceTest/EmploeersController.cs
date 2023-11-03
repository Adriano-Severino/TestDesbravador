using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Modelo.Application.Controllers;
using Modelo.Application.Models;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Service.Services;
using Modelo.Service.Validators;
using Moq;

public class EmploeersController
{
    [Fact]
    public async Task Create_ValidInput_ReturnsOkResult()
    {

        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var baseServiceMock = fixture.Freeze<Mock<IBaseService<Employees>>>();
        var createEmployeestModel = fixture.Create<CreateEmployeesModel>();
        var employeesModel = fixture.Create<EmployeesModel>();
        var tokenService = fixture.Freeze<Mock<ITokenService>>();
        var serviceEmployees = fixture.Freeze<Mock<IServiceEmployees>>();

        baseServiceMock.Setup(s => s.AddAsync<CreateEmployeesModel, EmployeesModel, EmployeesValidator>(createEmployeestModel))
            .ReturnsAsync(employeesModel);

        var controller = new EmployeesController(baseServiceMock.Object, tokenService.Object, serviceEmployees.Object);

        // Act
        var result = await controller.CreateAsync(createEmployeestModel);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var baseServiceMock = fixture.Freeze<Mock<IBaseService<Employees>>>();
        var updateEmployeesModel = fixture.Create<UpdateEmployeesModel>();
        var projectModel = fixture.Create<EmployeesModel>();
        var tokenService = fixture.Freeze<Mock<ITokenService>>();
        var serviceEmployees = fixture.Freeze<Mock<IServiceEmployees>>();

        baseServiceMock.Setup(s => s.UpdateAsync<UpdateEmployeesModel, EmployeesModel, EmployeesValidator>(updateEmployeesModel))
            .ReturnsAsync(projectModel);

        var controller = new EmployeesController(baseServiceMock.Object, tokenService.Object, serviceEmployees.Object);

        // Act
        var result = await controller.UpdateAsync(updateEmployeesModel);

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

        var baseServiceMock = fixture.Freeze<Mock<IBaseService<Employees>>>();
        var updateemployeesModel = fixture.Create<UpdateEmployeesModel>();
        var employees = fixture.Create<List<Employees>>();
        var tokenService = fixture.Freeze<Mock<ITokenService>>();

         baseServiceMock.Setup(s => s.GetAsync<Employees>())
            .ReturnsAsync(employees);

        var controller = new EmployeesController(baseServiceMock.Object, tokenService.Object, null); ;

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
        var baseEmployeesServiceMock = fixture.Freeze<Mock<IBaseService<Employees>>>();
        var id = fixture.Create<Guid>();
        var expectedEmployeesModel = fixture.Create<EmployeesModel>();
        var tokenService = fixture.Freeze<Mock<ITokenService>>();
        var serviceEmployees = fixture.Freeze<Mock<IServiceEmployees>>();

        baseEmployeesServiceMock.Setup(s => s.GetByIdAsync<EmployeesModel>(id)).ReturnsAsync(expectedEmployeesModel);

        var controller = new EmployeesController(baseEmployeesServiceMock.Object, tokenService.Object, serviceEmployees.Object);

        // Act
        var result = await controller.GetAsync(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<EmployeesModel>(okResult.Value);
        Assert.Equal(expectedEmployeesModel.Id, returnValue.Id);
    }
}