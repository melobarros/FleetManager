namespace FleetManager.Domain.Entities
{
    public class Truck : Vehicle
    {
        protected Truck() { }
        public Truck(string chassisSeries, uint chassisNumber, string color)
            : base(chassisSeries, chassisNumber, color) { }

        public override int NumberOfPassengers => 1;
    }
}
