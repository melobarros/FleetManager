namespace FleetManager.Application.DTOs
{
    public class CreateVehicleRequest
    {
        public string ChassisSeries { get; set; }
        public uint ChassisNumber { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
    }
}