namespace FleetManager.Domain.Entities.Diagnostics;

public class Sensor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public int MinThreshold { get; set; }
    public int MaxThreshold { get; set; }

    public int ProtocolId { get; set; }
    public DiagnosticProtocol Protocol { get; set; }

    public int ErrorCodeId { get; set; }
    public ErrorCode ErrorCode { get; set; }
}