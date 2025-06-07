using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;
using FleetManager.Domain.ValueObjects;

namespace FleetManager.Domain.Factories
{
    public static class VehicleFactory
    {
        public static Vehicle Create(VehicleType type, ChassisId chassisId, string color)
        {
            return type switch
            {
                VehicleType.Car => new Car(chassisId, color),
                VehicleType.Truck => new Truck(chassisId, color),
                VehicleType.Bus => new Bus(chassisId, color),
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown vehicle type")
            };
        }

    }
}