using FleetManager.Application.DTOs;
using FleetManager.Application.Services;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.Diagnostics;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Interfaces;
using FleetManager.Infrastructure.EntityFramework.Data;
using FleetManager.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace FleetManager.Tests.Services
{
    public class VehicleAppServiceTests : IClassFixture<TestFixture>, IAsyncLifetime
    {
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly Mock<IVehicleFactoryAppService> _vehicleFactoryAppServiceMock;
        private readonly VehicleAppService _vehicleAppService;
        private readonly IVehicleAppService _vehicleAppServiceFixture;
        private readonly AppDbContext _dbContext;

        public VehicleAppServiceTests(TestFixture fixture)
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _vehicleFactoryAppServiceMock = new Mock<IVehicleFactoryAppService>();
            _vehicleAppService = new VehicleAppService(_vehicleRepositoryMock.Object, _vehicleFactoryAppServiceMock.Object);

            var provider = fixture.Provider;
            _vehicleAppServiceFixture = provider.GetRequiredService<IVehicleAppService>();
            _dbContext = provider.GetRequiredService<AppDbContext>();
        }


        public async Task InitializeAsync()
        {
            _dbContext.Vehicles.RemoveRange(_dbContext.Vehicles);
            await _dbContext.SaveChangesAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public void Create_ShouldReturnDto_WhenValid()
        {
            var dto = new VehicleDto
            {
                ChassisSeries = "VLV-C",
                ChassisNumber = 1,
                Type = nameof(VehicleType.Car),
                Color = "Red"
            };

            var result = _vehicleAppServiceFixture.Create(dto);

            Assert.Equal("VLV-C", result.ChassisSeries);
            Assert.Equal((uint)1, result.ChassisNumber);
            Assert.Equal("Car", result.Type);
            Assert.Equal("Red", result.Color);
            Assert.Equal(4, result.NumberOfPassengers);
        }

        [Fact]
        public void Create_ShouldThrow_WhenDuplicate()
        {
            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV", 1))
                     .Returns(new Car("VLV", 1, "Blue"));

            var dto = new VehicleDto
            {
                ChassisSeries = "VLV",
                ChassisNumber = 1,
                Type = nameof(VehicleType.Car),
                Color = "Red"
            };

            var ex = Assert.Throws<InvalidOperationException>(() => _vehicleAppService.Create(dto));
            Assert.Contains("already exists", ex.Message);
            _vehicleRepositoryMock.Verify(r => r.Add(It.IsAny<Vehicle>()), Times.Never);
        }

        [Fact]
        public void ChangeColor_ShouldReturnUpdatedDto_WhenExists()
        {
            var createdVehicle = CreateNewTestVehicle("VLV-T", 1, "Truck", "Red");
            var result = _vehicleAppServiceFixture.ChangeColor(createdVehicle.ChassisSeries, createdVehicle.ChassisNumber, "Black");

            Assert.Equal("Black", result.Color);
            Assert.Equal(1, result.NumberOfPassengers);
        }

        [Fact]
        public void ChangeColor_ShouldThrowNotFound_WhenMissing()
        {
            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV", 2)).Returns((Vehicle?)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _vehicleAppService.ChangeColor("VLV", 2, "Any"));
            Assert.Contains("not found", ex.Message, StringComparison.OrdinalIgnoreCase);
            _vehicleRepositoryMock.Verify(r => r.Update(It.IsAny<Vehicle>()), Times.Never);
        }

        [Fact]
        public void GetByChassis_ShouldReturnDto_WhenExists()
        {
            var createdVehicle = CreateNewTestVehicle("VLV-B", 2, "Bus", "Red");
            var result = _vehicleAppServiceFixture.GetByChassis(createdVehicle.ChassisSeries, createdVehicle.ChassisNumber);

            Assert.Equal(createdVehicle.ChassisSeries, result.ChassisSeries);
            Assert.Equal(createdVehicle.ChassisNumber, result.ChassisNumber);
            Assert.Equal(createdVehicle.Type, result.Type);
            Assert.Equal(createdVehicle.Color, result.Color);
            Assert.Equal(createdVehicle.NumberOfPassengers, result.NumberOfPassengers);
        }

        [Fact]
        public void GetByChassis_ShouldThrowNotFound_WhenMissing()
        {
            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV", 9)).Returns((Vehicle?)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _vehicleAppService.GetByChassis("VLV", 9));
            Assert.Contains("not found", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GetAll_ShouldReturnAllDtos()
        {
            var createdVehicle1 = CreateNewTestVehicle("VLV-C", 5, "Car", "Red");
            var createdVehicle2 = CreateNewTestVehicle("VLV-C", 6, "Car", "Red");

            var result = _vehicleAppServiceFixture.GetAll();

            Assert.Collection(result,
                dto =>
                {
                    Assert.Equal(createdVehicle1.ChassisSeries, dto.ChassisSeries);
                    Assert.Equal(createdVehicle1.ChassisNumber, dto.ChassisNumber);
                    Assert.Equal(createdVehicle1.Type, dto.Type);
                    Assert.Equal(createdVehicle1.Color, dto.Color);
                    Assert.Equal(createdVehicle1.NumberOfPassengers, dto.NumberOfPassengers);
                },
                dto =>
                {
                    Assert.Equal(createdVehicle2.ChassisSeries, dto.ChassisSeries);
                    Assert.Equal(createdVehicle2.ChassisNumber, dto.ChassisNumber);
                    Assert.Equal(createdVehicle2.Type, dto.Type);
                    Assert.Equal(createdVehicle2.Color, dto.Color);
                    Assert.Equal(createdVehicle2.NumberOfPassengers, dto.NumberOfPassengers);
                });
        }

        [Fact]
        public void Delete_ShouldReturnDto_WhenExists()
        {
            var createdVehicle = CreateNewTestVehicle("VLV-B", 7, "Bus", "Red");
            var result = _vehicleAppServiceFixture.Delete(createdVehicle.ChassisSeries, createdVehicle.ChassisNumber);

            Assert.Equal(createdVehicle.ChassisSeries, result.ChassisSeries);
            Assert.Equal(createdVehicle.ChassisNumber, result.ChassisNumber);
        }

        [Fact]
        public void Delete_ShouldThrowNotFound_WhenMissing()
        {
            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV", 9)).Returns((Vehicle?)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _vehicleAppService.Delete("VLV", 9));
            Assert.Contains("not found", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        private VehicleDto CreateNewTestVehicle(string chassisSeries, uint chassisNumber, string type, string color)
        {
            var dto = new VehicleDto
            {
                ChassisSeries = chassisSeries,
                ChassisNumber = chassisNumber,
                Type = type,
                Color = color
            };

            var createdVehicle = _vehicleAppServiceFixture.Create(dto);

            foreach (var entry in _dbContext.ChangeTracker.Entries()
                .Where(e =>
                    (e.Entity is Vehicle v && v.ChassisSeries == createdVehicle.ChassisSeries && v.ChassisNumber == createdVehicle.ChassisNumber) ||
                    (e.Entity is DiagnosticProtocol p && p.Name == createdVehicle.DiagnosticProtocol)))
            {
                entry.State = EntityState.Detached;
            }

            return createdVehicle;
        }
    }
}
