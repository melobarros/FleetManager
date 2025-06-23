using FleetManager.Domain.Entities.Diagnostics;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Interfaces
{
    public interface IProtocolRepository
    {
        DiagnosticProtocol? GetByVehicleType(VehicleType vehicleType);
    }
}