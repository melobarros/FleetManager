using FleetManager.Domain.ValueObjects;

namespace FleetManager.Domain.Entities
{
    public class Bus : Vehicle
    {
        public override int NumberOfPassengers => 42;
        public Bus(ChassisId chassisId, string color)
            : base(chassisId, color) { }
    }
}
