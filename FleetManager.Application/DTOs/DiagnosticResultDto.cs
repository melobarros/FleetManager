using FleetManager.Domain.Enums;

namespace FleetManager.Application.DTOs;

public class DiagnosticResultDto 
{
    public VehicleType VehicleType { get; set; }
    public DateTime ExecutionDate { get; set; } = DateTime.Now;
    public List<SensorReading> Readings { get; set; } = new();
    public List<DetectedError> Errors { get; set; } = new();
}

public class SensorReading
{
    public string Sensor { get; set; }
    public double Value { get; set; }

    public SensorReading(string sensor, double value)
    {
        Sensor = sensor;
        Value = value;
    }
}

public class DetectedError
{
    public string Code { get; set; }
    public string Description { get; set; }

    public DetectedError(string code, string description)
    {
        Code = code;
        Description = description;
    }
}
