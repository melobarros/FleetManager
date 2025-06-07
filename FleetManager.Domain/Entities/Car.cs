using FleetManager.Domain.ValueObjects;

namespace FleetManager.Domain.Entities
{
    public class Car : Vehicle
    {
        public Car() { }

        public override int NumberOfPassengers => 4;
        public Car(ChassisId chassisId, string color)
            : base(chassisId, color) { }
    }
}
