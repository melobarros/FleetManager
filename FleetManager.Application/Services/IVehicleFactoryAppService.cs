using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;

namespace FleetManager.Application.Services
{
    public interface IVehicleFactoryAppService
    {
        Vehicle Create(VehicleType type, string chassisSeries, uint chassisNumber, string color);
    }
}
