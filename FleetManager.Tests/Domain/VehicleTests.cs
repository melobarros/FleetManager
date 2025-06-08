using FleetManager.Domain.Entities;

namespace FleetManager.Tests.Domain
{
    public class VehicleTests
    {
        [Fact]
        public void Car_ShouldHave4Passengers()
        {
            var car = new Car("VLV", 1, "Red");
            Assert.Equal(4, car.NumberOfPassengers);
        }

        [Fact]
        public void Truck_ShouldHave1Passenger()
        {
            var truck = new Truck("VLV", 2, "Blue");
            Assert.Equal(1, truck.NumberOfPassengers);
        }

        [Fact]
        public void Bus_ShouldHave42Passengers()
        {
            var bus = new Bus("VLV", 3, "Green");
            Assert.Equal(42, bus.NumberOfPassengers);
        }

        [Theory]
        [InlineData("", 1)]
        [InlineData(null, 1)]
        public void Constructor_ShouldThrowArgumentNullException_WhenSeriesIsNullOrEmpty(string chassisSeries, uint chassisNumber)
        {
            Assert.Throws<ArgumentNullException>(() => new Car(chassisSeries!, chassisNumber, "Red"));
        }

        [Theory]
        [InlineData("VLV", "")]
        [InlineData("VLV", null)]
        public void Constructor_ShouldThrowArgumentNullException_WhenColorIsNullOrEmpty(string chassisSeries, string color)
        {
            Assert.Throws<ArgumentNullException>(() => new Bus(chassisSeries!, 1, color));
        }

        [Fact]
        public void ChangeColor_ShouldUpdateColor_WhenValid()
        {
            var truck = new Truck("VLV", 8, "Purple");
            truck.ChangeColor("White");
            
            Assert.Equal("White", truck.Color);
        }
    }
}
