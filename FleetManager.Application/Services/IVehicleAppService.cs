using FleetManager.Application.DTOs;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Services
{
    public interface IVehicleAppService
    {
        VehicleDto Create(VehicleDto dto);
        VehicleDto ChangeColor(string chassisSeries, uint chassisNumber, string newColor);
        VehicleDto GetByChassis(string chassisSeries, uint chassisNumber);
        IEnumerable<VehicleDto> GetAll();
        VehicleDto Delete(string chassisSeries, uint chassisNumber);
    }
}