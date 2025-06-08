using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Tests.Domain
{
    public class VehicleFactoryTests
    {
        [Theory]
        [InlineData(VehicleType.Car, typeof(Car))]
        [InlineData(VehicleType.Truck, typeof(Truck))]
        [InlineData(VehicleType.Bus, typeof(Bus))]
        public void Create_ShouldReturnCorrectSubclass_ForEachVehicleType(VehicleType type, Type expectedType)
        {
            var vehicle = VehicleFactory.Create(type, "S1", 1, "Red");
            Assert.IsType(expectedType, vehicle);
        }

        [Fact]
        public void Create_ShouldThrowArgumentOutOfRangeException_OnUnknownVehicleType()
        {
            var invalidType = (VehicleType)999;
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                VehicleFactory.Create(invalidType, "X", 1, "Blue"));
        }
    }
}
