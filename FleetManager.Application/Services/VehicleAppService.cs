using FleetManager.Application.DTOs;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Factories;
using FleetManager.Domain.Interfaces;
using FleetManager.Domain.ValueObjects;

namespace FleetManager.Application.Services
{
    public class VehicleAppService : IVehicleAppService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleAppService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public VehicleDto Create(VehicleDto dto)
        {
            var chassisId = new ChassisId(dto.ChassisSeries, dto.ChassisNumber);
            var vehicleExists = _vehicleRepository.GetByChassis(chassisId) != null;

            if (vehicleExists)
                throw new InvalidOperationException("A vehicle with this chassis already exists.");

            var vehicleType = Enum.Parse<VehicleType>(dto.Type, ignoreCase: true);
            var vehicle = VehicleFactory.Create(vehicleType, chassisId, dto.Color);

            _vehicleRepository.Add(vehicle);
            return ToDto(vehicle);
        }

        public VehicleDto ChangeColor(string chassisSeries, uint chassisNumber, string newColor)
        {
            var chassisId = new ChassisId(chassisSeries, chassisNumber);
            var vehicle = _vehicleRepository.GetByChassis(chassisId);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            vehicle.ChangeColor(newColor);
            _vehicleRepository.Update(vehicle);

            return ToDto(vehicle);
        }

        public VehicleDto GetByChassis(string chassisSeries, uint chassisNumber)
        {
            var chassisId = new ChassisId(chassisSeries, chassisNumber);
            var vehicle = _vehicleRepository.GetByChassis(chassisId);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            return ToDto(vehicle);
        }

        public IEnumerable<VehicleDto> GetAll()
        {
            return _vehicleRepository.GetAll().Select(v => ToDto(v));
        }

        public VehicleDto Delete(string chassisSeries, uint chassisNumber) 
        {
            var chassisId = new ChassisId(chassisSeries, chassisNumber);
            var vehicle = _vehicleRepository.GetByChassis(chassisId);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            _vehicleRepository.Delete(vehicle);

            return ToDto(vehicle);
        }

        private static VehicleDto ToDto(Vehicle vehicle)
        {
            return new VehicleDto
            {
                ChassisSeries       = vehicle.ChassisId.Series,
                ChassisNumber       = vehicle.ChassisId.Number,
                Type                = vehicle.GetType().Name,
                Color               = vehicle.Color,
                NumberOfPassengers  = vehicle.NumberOfPassengers
            };
        }
    }
}
