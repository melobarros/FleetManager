using FleetManager.Domain.ValueObjects;

namespace FleetManager.Domain.Entities
{
    public class Truck : Vehicle
    {
        public override int NumberOfPassengers => 1;
        public Truck(ChassisId chassisId, string color)
            : base(chassisId, color) { }
    }
}
