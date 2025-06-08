namespace FleetManager.Domain.Entities
{
    public class Car : Vehicle
    {
        protected Car() { }
        public Car(string chassisSeries, uint chassisNumber, string color)
            : base(chassisSeries, chassisNumber, color) { }

        public override int NumberOfPassengers => 4;
    }
}
