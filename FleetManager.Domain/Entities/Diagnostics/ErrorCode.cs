namespace FleetManager.Domain.Entities.Diagnostics;

public class ErrorCode 
{
    public int Id { get; set; } 
    public string Code { get; set; }
    public string Description { get; set; }
    public int ProtocolId { get; set; }
    public DiagnosticProtocol Protocol { get; set; }
}