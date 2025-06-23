using FleetManager.Application.DTOs;
using FleetManager.Application.Services;
using FleetManager.Domain.Entities.Diagnostics;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Interfaces;
using Moq;

namespace FleetManager.Tests.Services
{
    public class VehicleFactoryAppServiceTests
    {
        private readonly Mock<IProtocolRepository> _protocolRepositoryMock;
        private readonly VehicleFactoryAppService _service;

        public VehicleFactoryAppServiceTests()
        {
            _protocolRepositoryMock = new Mock<IProtocolRepository>();
            _service = new VehicleFactoryAppService(_protocolRepositoryMock.Object);
        }

        [Fact]
        public void Create_ShouldReturnVehicleWithProtocol_WhenProtocolExists()
        {
            var vehicleType = VehicleType.Car;
            var expectedProtocol = new DiagnosticProtocol
            {
                Id = 1,
                Name = "Car Protocol",
                VehicleType = vehicleType
            };

            var dto = new VehicleDto
            {
                ChassisSeries = "VLV-C",
                ChassisNumber = 1,
                Type = nameof(vehicleType),
                Color = "Red"
            };

            _protocolRepositoryMock.Setup(r => r.GetByVehicleType(vehicleType)).Returns(expectedProtocol);

            var vehicle = _service.Create(vehicleType, dto.ChassisSeries, dto.ChassisNumber, dto.Color);

            Assert.NotNull(vehicle);
            Assert.Equal(dto.ChassisSeries, vehicle.ChassisSeries);
            Assert.Equal(dto.ChassisNumber, vehicle.ChassisNumber);
            Assert.Equal(dto.Color, vehicle.Color);
            Assert.Equal(vehicleType.ToString(), vehicle.GetType().Name);
            Assert.Equal(expectedProtocol, vehicle.DiagnosticProtocol);
            _protocolRepositoryMock.Verify(r => r.GetByVehicleType(vehicleType), Times.Once);
        }

        [Fact]
        public void Create_ShouldThrowInvalidOperationException_WhenProtocolNotFound()
        {
            var type = VehicleType.Car;

            _protocolRepositoryMock.Setup(r => r.GetByVehicleType(type)).Returns((DiagnosticProtocol?)null);

            var ex = Assert.Throws<InvalidOperationException>(() => _service.Create(type, "CAR", 1, "Red"));

            Assert.Equal("Diagnostic protocol not found for vehicle type", ex.Message);
            _protocolRepositoryMock.Verify(r => r.GetByVehicleType(type), Times.Once);
        }
    }
}