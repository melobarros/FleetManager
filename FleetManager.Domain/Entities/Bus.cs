namespace FleetManager.Domain.Entities
{
    public class Bus : Vehicle
    {
        protected Bus() { }
        public Bus(string chassisSeries, uint chassisNumber, string color)
            : base(chassisSeries, chassisNumber, color) { }

        public override int NumberOfPassengers => 42;
    }
}
