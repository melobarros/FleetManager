using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.Diagnostics;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Factories
{
    public static class VehicleFactory
    {
        public static Vehicle Create(VehicleType type, string chassisSeries, uint chassisNumber, string color, DiagnosticProtocol protocol)
        {
            return type switch
            {
                VehicleType.Car => new Car(chassisSeries, chassisNumber, color, protocol),
                VehicleType.Truck => new Truck(chassisSeries, chassisNumber, color, protocol),
                VehicleType.Bus => new Bus(chassisSeries, chassisNumber, color, protocol),
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown vehicle type")
            };
        }
    }
}