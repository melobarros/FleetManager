using FleetManager.Application.Services;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.Diagnostics;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Interfaces;
using Moq;

namespace FleetManager.Tests.Services
{
    public class DiagnosticAppServiceTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly DiagnosticAppService _service;

        public DiagnosticAppServiceTests()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _service = new DiagnosticAppService(_vehicleRepositoryMock.Object);
        }

        [Fact]
        public void RunDiagnostic_ShouldReturnResultDto_WhenProtocolExists()
        {
            var vehicleType = VehicleType.Car;
            var expectedProtocol = new DiagnosticProtocol
            {
                Id = 1,
                Name = "Car Protocol",
                VehicleType = vehicleType,
                Sensors = new List<Sensor>()
            };

            var vehicle = new Car("VLV-C", 1, "Red");
            vehicle.SetDiagnosticProtocol(expectedProtocol);

            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV-C", 1)).Returns(vehicle);

            var result = _service.RunDiagnostic("VLV-C", 1);

            Assert.Equal(vehicleType, result.VehicleType);
            Assert.Empty(result.Readings);
            Assert.Empty(result.Errors);
            Assert.True((DateTime.Now - result.ExecutionDate) < TimeSpan.FromSeconds(1));
            _vehicleRepositoryMock.Verify(r => r.GetByChassis("VLV-C", 1), Times.Once);
        }

        [Fact]
        public void RunDiagnostic_ShouldThrowInvalidOperationException_WhenProtocolNotAssigned()
        {
            var vehicle = new Car("VLV-C", 2, "Blue");

            _vehicleRepositoryMock
                .Setup(r => r.GetByChassis("VLV-C", 2))
                .Returns(vehicle);

            var ex = Assert.Throws<InvalidOperationException>(() => _service.RunDiagnostic("VLV-C", 2));

            Assert.Equal("Diagnostic protocol not assigned to vehicle.", ex.Message);
            _vehicleRepositoryMock.Verify(r => r.GetByChassis("VLV-C", 2), Times.Once);
        }
    }
}