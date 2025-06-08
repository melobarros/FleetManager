using FleetManager.Application.DTOs;
using FleetManager.Application.Services;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Interfaces;
using Moq;

namespace FleetManager.Tests.Services
{
    public  class VehicleAppServiceTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly VehicleAppService _vehicleAppService;

        public VehicleAppServiceTests()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _vehicleAppService = new VehicleAppService(_vehicleRepositoryMock.Object);
        }

        [Fact]
        public void Create_ShouldReturnDto_WhenValid()
        {
            var dto = new VehicleDto
            {
                ChassisSeries = "VLV",
                ChassisNumber = 1,
                Type = nameof(VehicleType.Car),
                Color = "Red"
            };
            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV", 1)).Returns((Vehicle?)null);

            var result = _vehicleAppService.Create(dto);

            Assert.Equal("VLV", result.ChassisSeries);
            Assert.Equal((uint)1, result.ChassisNumber);
            Assert.Equal("Car", result.Type);
            Assert.Equal("Red", result.Color);
            Assert.Equal(4, result.NumberOfPassengers);
            _vehicleRepositoryMock.Verify(r => r.Add(It.IsAny<Vehicle>()), Times.Once);
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
            var existing = new Truck("VLV", 2, "White");
            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV", 2)).Returns(existing);

            var result = _vehicleAppService.ChangeColor("VLV", 2, "Black");

            Assert.Equal("Black", result.Color);
            Assert.Equal(1, result.NumberOfPassengers);
            _vehicleRepositoryMock.Verify(r => r.Update(It.Is<Vehicle>(v => v.Color == "Black")), Times.Once);
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
            var bus = new Bus("VLV", 5, "Green");
            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV", 5)).Returns(bus);

            var result = _vehicleAppService.GetByChassis("VLV", 5);

            Assert.Equal("VLV", result.ChassisSeries);
            Assert.Equal((uint)5, result.ChassisNumber);
            Assert.Equal("Bus", result.Type);
            Assert.Equal("Green", result.Color);
            Assert.Equal(42, result.NumberOfPassengers);
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
            var vehicles = new List<Vehicle>
            {
                new Car("VLV", 1, "Red"),
                new Truck("VLV", 2, "Blue")
            };
            _vehicleRepositoryMock.Setup(r => r.GetAll()).Returns(vehicles);

            var result = _vehicleAppService.GetAll();

            Assert.Collection(result,
                dto =>
                {
                    Assert.Equal("VLV", dto.ChassisSeries);
                    Assert.Equal((uint)1, dto.ChassisNumber);
                    Assert.Equal("Car", dto.Type);
                    Assert.Equal("Red", dto.Color);
                    Assert.Equal(4, dto.NumberOfPassengers);
                },
                dto =>
                {
                    Assert.Equal("VLV", dto.ChassisSeries);
                    Assert.Equal((uint)2, dto.ChassisNumber);
                    Assert.Equal("Truck", dto.Type);
                    Assert.Equal("Blue", dto.Color);
                    Assert.Equal(1, dto.NumberOfPassengers);
                });
        }

        [Fact]
        public void Delete_ShouldReturnDto_WhenExists()
        {
            var car = new Car("VLV", 3, "Yellow");
            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV", 3)).Returns(car);

            var result = _vehicleAppService.Delete("VLV", 3);

            Assert.Equal("VLV", result.ChassisSeries);
            Assert.Equal((uint)3, result.ChassisNumber);
            _vehicleRepositoryMock.Verify(r => r.Delete(It.Is<Vehicle>(v => v.ChassisSeries == "VLV" && v.ChassisNumber == 3)), Times.Once);
        }

        [Fact]
        public void Delete_ShouldThrowNotFound_WhenMissing()
        {
            _vehicleRepositoryMock.Setup(r => r.GetByChassis("VLV", 9)).Returns((Vehicle?)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _vehicleAppService.Delete("VLV", 9));
            Assert.Contains("not found", ex.Message, StringComparison.OrdinalIgnoreCase);
        }
    }
}
