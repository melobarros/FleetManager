using FleetManager.Domain.Entities.Diagnostics;

namespace FleetManager.Domain.Entities
{
    public class Car : Vehicle
    {
        protected Car() { }

        public Car(string chassisSeries, uint chassisNumber, string color, DiagnosticProtocol protocol)
            : base(chassisSeries, chassisNumber, color, protocol) { }

        public override int NumberOfPassengers => 4;
    }
}