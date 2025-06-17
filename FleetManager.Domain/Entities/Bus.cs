using FleetManager.Domain.Entities.Diagnostics;

namespace FleetManager.Domain.Entities
{
    public class Bus : Vehicle
    {
        protected Bus() { }

        public Bus(string chassisSeries, uint chassisNumber, string color, DiagnosticProtocol protocol)
            : base(chassisSeries, chassisNumber, color, protocol) { }

        public override int NumberOfPassengers => 42;
    }
}