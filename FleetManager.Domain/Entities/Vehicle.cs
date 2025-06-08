namespace FleetManager.Domain.Entities
{
    public abstract class Vehicle
    {
        public string ChassisSeries { get; private set; }
        public uint ChassisNumber { get; private set; }
        public string Color { get; private set; }
        public abstract int NumberOfPassengers { get; }

        protected Vehicle() { }
        protected Vehicle(string chassisSeries, uint chassisNumber, string color)
        {
            if(string.IsNullOrEmpty(chassisSeries))
                throw new ArgumentNullException("Chassis Series is required.");
            if(string.IsNullOrEmpty(color))
                throw new ArgumentNullException("Color is required.");

            ChassisSeries = chassisSeries;
            ChassisNumber = chassisNumber;
            Color = color;
        }

        public void ChangeColor(string newColor)
        {
            if (string.IsNullOrEmpty(newColor))
                throw new ArgumentNullException("New Color is required.");

            Color = newColor;
        }
    }
}
