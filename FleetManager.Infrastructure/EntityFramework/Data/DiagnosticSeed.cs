using Microsoft.EntityFrameworkCore;
using FleetManager.Domain.Entities.Diagnostics;
using FleetManager.Domain.Enums;

namespace FleetManager.Infrastructure.EntityFramework.Data;

public static class DiagnosticSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Protocolos
        modelBuilder.Entity<DiagnosticProtocol>().HasData(
            new DiagnosticProtocol { Id = 1, Name = "Truck Diagnostic Protocol", VehicleType = VehicleType.Truck },
            new DiagnosticProtocol { Id = 2, Name = "Bus Diagnostic Protocol", VehicleType = VehicleType.Bus }
        );

        // Sensores Truck (mais realista agora)
        modelBuilder.Entity<Sensor>().HasData(
            new Sensor { Id = 1, Name = "Engine Coolant Temperature", Unit = "°C", ProtocolId = 1 },
            new Sensor { Id = 2, Name = "Engine Speed (RPM)", Unit = "RPM", ProtocolId = 1 },
            new Sensor { Id = 3, Name = "Oil Pressure", Unit = "Bar", ProtocolId = 1 },
            new Sensor { Id = 4, Name = "Fuel Level", Unit = "%", ProtocolId = 1 },
            new Sensor { Id = 5, Name = "Battery Voltage", Unit = "V", ProtocolId = 1 },
            new Sensor { Id = 6, Name = "Brake Air Pressure", Unit = "Bar", ProtocolId = 1 },
            new Sensor { Id = 7, Name = "Transmission Oil Temperature", Unit = "°C", ProtocolId = 1 }
        );

        // Sensores Bus
        modelBuilder.Entity<Sensor>().HasData(
            new Sensor { Id = 8, Name = "Engine Coolant Temperature", Unit = "°C", ProtocolId = 2 },
            new Sensor { Id = 9, Name = "Engine Speed (RPM)", Unit = "RPM", ProtocolId = 2 },
            new Sensor { Id = 10, Name = "Fuel Level", Unit = "%", ProtocolId = 2 },
            new Sensor { Id = 11, Name = "Passenger Door Status", Unit = "", ProtocolId = 2 },
            new Sensor { Id = 12, Name = "AC Compressor Status", Unit = "", ProtocolId = 2 },
            new Sensor { Id = 13, Name = "Brake Air Pressure", Unit = "Bar", ProtocolId = 2 },
            new Sensor { Id = 14, Name = "Wheel Speed Front Axle", Unit = "km/h", ProtocolId = 2 }
        );

        // Códigos de erro Truck (realistas simulados)
        modelBuilder.Entity<ErrorCode>().HasData(
            new ErrorCode { Id = 1, Code = "T001", Description = "Engine Overheating", ProtocolId = 1 },
            new ErrorCode { Id = 2, Code = "T002", Description = "Low Oil Pressure", ProtocolId = 1 },
            new ErrorCode { Id = 3, Code = "T003", Description = "Fuel System Leak", ProtocolId = 1 },
            new ErrorCode { Id = 4, Code = "T004", Description = "Brake Air Pressure Low", ProtocolId = 1 },
            new ErrorCode { Id = 5, Code = "T005", Description = "Battery Voltage Low", ProtocolId = 1 },
            new ErrorCode { Id = 6, Code = "T006", Description = "Transmission Oil Overheating", ProtocolId = 1 }
        );

        // Códigos de erro Bus (realistas simulados)
        modelBuilder.Entity<ErrorCode>().HasData(
            new ErrorCode { Id = 7, Code = "B001", Description = "Passenger Door Sensor Fault", ProtocolId = 2 },
            new ErrorCode { Id = 8, Code = "B002", Description = "AC Compressor Failure", ProtocolId = 2 },
            new ErrorCode { Id = 9, Code = "B003", Description = "Brake Air Pressure Low", ProtocolId = 2 },
            new ErrorCode { Id = 10, Code = "B004", Description = "Wheel Speed Sensor Failure", ProtocolId = 2 },
            new ErrorCode { Id = 11, Code = "B005", Description = "Communication Bus Error", ProtocolId = 2 }
        );
    }
}