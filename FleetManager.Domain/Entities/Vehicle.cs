namespace FleetManager.Domain.Entities
{
    public abstract class Vehicle
    {
        public string ChassisId { get; private set; }
        public string Color { get; private set; }
        public abstract int NumberOfPassengers { get; }

        protected Vehicle(string chassisId, string color)
        {
            if(string.IsNullOrEmpty(chassisId))
                throw new ArgumentNullException("ChassisId is required.");
            if(string.IsNullOrEmpty(color))
                throw new ArgumentNullException("Color is required.");

            ChassisId = chassisId;
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
