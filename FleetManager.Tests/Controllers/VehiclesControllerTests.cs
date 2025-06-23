using FleetManager.API.Controllers;
using FleetManager.Application.DTOs;
using FleetManager.Application.Services;
using FleetManager.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FleetManager.Tests.Controllers
{
    public class VehiclesControllerTests
    {
        private readonly Mock<IVehicleAppService> _appServiceMock;
        private readonly Mock<IDiagnosticAppService> _diagnosticAppServiceMock;
        private readonly VehiclesController _vehiclesController;

        public VehiclesControllerTests()
        {
            _appServiceMock = new Mock<IVehicleAppService>();
            _diagnosticAppServiceMock = new Mock<IDiagnosticAppService>();
            _vehiclesController = new VehiclesController(_appServiceMock.Object, _diagnosticAppServiceMock.Object);
        }

        [Fact]
        public void GetByChassis_ReturnsOk_WhenVehicleExists()
        {
            var dto = new VehicleDto
            {
                ChassisSeries = "S1",
                ChassisNumber = 1,
                Type = "Car",
                Color = "Red",
                NumberOfPassengers = 4
            };
            _appServiceMock.Setup(s => s.GetByChassis("S1", 1))
                        .Returns(dto);

            var result = _vehiclesController.GetByChassis("S1", 1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<VehicleDto>(ok.Value);
            Assert.Equal("S1", returned.ChassisSeries);
            Assert.Equal((uint)1, returned.ChassisNumber);
        }

        [Fact]
        public void GetByChassis_ThrowsKeyNotFoundException_WhenVehicleNotFound()
        {
            _appServiceMock.Setup(s => s.GetByChassis("X", 2))
                        .Throws(new KeyNotFoundException("Vehicle not found."));

            Assert.Throws<KeyNotFoundException>(() => _vehiclesController.GetByChassis("X", 2));
        }

        [Fact]
        public void Create_ReturnsBadRequest_WhenDtoIsNull()
        {
            var result = _vehiclesController.Create(null);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Vehicle data is required.", bad.Value);
        }

        [Fact]
        public void Create_ReturnsCreatedAtAction_WhenValid()
        {
            var createRequest = new CreateVehicleRequest
            {
                ChassisSeries = "C2",
                ChassisNumber = 2,
                Type = "Truck",
                Color = "Blue",
            };

            var dto = new VehicleDto
            {
                ChassisSeries = "C2",
                ChassisNumber = 2,
                Type = "Truck",
                Color = "Blue",
                NumberOfPassengers = 1
            };
            _appServiceMock.Setup(s => s.Create(createRequest))
                        .Returns(dto);

            var result = _vehiclesController.Create(createRequest);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(VehiclesController.GetByChassis), created.ActionName);
            Assert.Equal("C2", created.RouteValues["chassisSeries"]);
            Assert.Equal((uint)2, created.RouteValues["chassisNumber"]);
            var returned = Assert.IsType<VehicleDto>(created.Value);
            Assert.Same(dto, returned);
        }

        [Fact]
        public void GetAll_ReturnsOk_WithListOfVehicles()
        {
            var list = new List<VehicleDto>
            {
                new VehicleDto { ChassisSeries="A", ChassisNumber=1, Type="Car", Color="Red", NumberOfPassengers=4 },
                new VehicleDto { ChassisSeries="B", ChassisNumber=2, Type="Bus", Color="Green", NumberOfPassengers=42 }
            };
            _appServiceMock.Setup(s => s.GetAll()).Returns(list);

            var result = _vehiclesController.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<List<VehicleDto>>(ok.Value);
            Assert.Equal(2, returned.Count);
        }

        [Fact]
        public void ChangeColor_ReturnsOk_WithUpdatedDto()
        {
            var updated = new VehicleDto
            {
                ChassisSeries = "S3",
                ChassisNumber = 3,
                Type = "Car",
                Color = "Black",
                NumberOfPassengers = 4
            };
            _appServiceMock.Setup(s => s.ChangeColor("S3", 3, "Black"))
                        .Returns(updated);

            var result = _vehiclesController.ChangeColor("S3", 3, "Black");

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<VehicleDto>(ok.Value);
            Assert.Equal("Black", returned.Color);
        }

        [Fact]
        public void GetDiagnostic_ReturnsOk_WithDiagnosticResultDto()
        {
            var chassisSeries = "S1";
            uint chassisNumber = 1;
            var expectedDto = new DiagnosticResultDto
            {
                VehicleType = VehicleType.Car,
                ExecutionDate = DateTime.Now,
                Readings = new List<SensorReading>(),
                Errors = new List<DetectedError>()
            };
            _diagnosticAppServiceMock.Setup(s => s.RunDiagnostic(chassisSeries, chassisNumber)).Returns(expectedDto);

            var result = _vehiclesController.GetDiagnostic(chassisSeries, chassisNumber);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<DiagnosticResultDto>(ok.Value);
            Assert.Same(expectedDto, returned);
            _diagnosticAppServiceMock.Verify(s => s.RunDiagnostic(chassisSeries, chassisNumber), Times.Once);
        }

        [Fact]
        public void GetDiagnostic_ThrowsKeyNotFoundException_WhenVehicleNotFound()
        {
            var chassisSeries = "X";
            uint chassisNumber = 2;
            _diagnosticAppServiceMock.Setup(s => s.RunDiagnostic(chassisSeries, chassisNumber)).Throws(new KeyNotFoundException("Vehicle not found."));

            var ex = Assert.Throws<KeyNotFoundException>(() => _vehiclesController.GetDiagnostic(chassisSeries, chassisNumber));
            Assert.Equal("Vehicle not found.", ex.Message);
            _diagnosticAppServiceMock.Verify(s => s.RunDiagnostic(chassisSeries, chassisNumber), Times.Once);
        }
    }
}
