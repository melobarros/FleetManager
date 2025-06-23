using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities.Diagnostics;

public class DiagnosticProtocol 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public VehicleType VehicleType { get; set; }
    public ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
    public ICollection<ErrorCode> ErrorCodes { get; set; } = new List<ErrorCode>();
}