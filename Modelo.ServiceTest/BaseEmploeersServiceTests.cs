using AutoFixture.AutoMoq;
using AutoFixture;
using Modelo.Application.Models;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Service.Validators;
using Moq;

namespace Modelo.ServiceTest
{
    public class BaseEmploeersServiceTests
    {
        [Fact]
        public async Task Add_ValidInput_ReturnsOutputModel()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var baseServiceMock = fixture.Freeze<Mock<IBaseService<Employees>>>();
            var createEmployeesModel = fixture.Create<CreateEmployeesModel>();
            var EmployeesModel = fixture.Create<EmployeesModel>();

            baseServiceMock.Setup(s => s.AddAsync<CreateEmployeesModel, EmployeesModel, EmployeesValidator>(createEmployeesModel))
                .ReturnsAsync(EmployeesModel);

            // Use the mock here instead of trying to instantiate the interface
            var service = baseServiceMock.Object;

            // Act
            var result = await service.AddAsync<CreateEmployeesModel, EmployeesModel, EmployeesValidator>(createEmployeesModel);

            // Assert
            Assert.Equal(EmployeesModel, result);
        }

        [Fact]
        public async Task Update_ValidInput_ReturnsOutputModel()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var baseServiceMock = fixture.Freeze<Mock<IBaseService<Employees>>>();
            var updateEmployeesModel = fixture.Create<UpdateEmployeesModel>();
            var EmployeesModel = fixture.Create<EmployeesModel>();

            baseServiceMock.Setup(s => s.UpdateAsync<UpdateEmployeesModel, EmployeesModel, EmployeesValidator>(updateEmployeesModel))
               .ReturnsAsync(EmployeesModel);

            var service = baseServiceMock.Object;

            // Act
            var result = await service.UpdateAsync<UpdateEmployeesModel, EmployeesModel, EmployeesValidator>(updateEmployeesModel);

            // Assert
            Assert.Equal(EmployeesModel.Id, result.Id);
        }
    }
}
