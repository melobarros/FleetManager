using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Factories
{
    public static class VehicleFactory
    {
        public static Vehicle Create(VehicleType type, string chassisSeries, uint chassisNumber, string color)
        {
            return type switch
            {
                VehicleType.Car => new Car(chassisSeries, chassisNumber, color),
                VehicleType.Truck => new Truck(chassisSeries, chassisNumber, color),
                VehicleType.Bus => new Bus(chassisSeries, chassisNumber, color),
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown vehicle type")
            };
        }

    }
}