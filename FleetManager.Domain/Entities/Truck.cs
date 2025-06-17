using FleetManager.Domain.Entities.Diagnostics;

namespace FleetManager.Domain.Entities
{
    public class Truck : Vehicle
    {
        protected Truck() { }

        public Truck(string chassisSeries, uint chassisNumber, string color, DiagnosticProtocol protocol)
            : base(chassisSeries, chassisNumber, color, protocol) { }

        public override int NumberOfPassengers => 1;
    }
}