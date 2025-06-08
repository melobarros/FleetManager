using FleetManager.Application.DTOs;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Factories;
using FleetManager.Domain.Interfaces;

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
            var vehicleExists = _vehicleRepository.GetByChassis(dto.ChassisSeries, dto.ChassisNumber) != null;

            if (vehicleExists)
                throw new InvalidOperationException("A vehicle with this chassis already exists.");

            var vehicleType = Enum.Parse<VehicleType>(dto.Type, ignoreCase: true);
            var vehicle = VehicleFactory.Create(vehicleType, dto.ChassisSeries, dto.ChassisNumber, dto.Color);

            _vehicleRepository.Add(vehicle);
            return ToDto(vehicle);
        }

        public VehicleDto ChangeColor(string chassisSeries, uint chassisNumber, string newColor)
        {
            var vehicle = _vehicleRepository.GetByChassis(chassisSeries, chassisNumber);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            vehicle.ChangeColor(newColor);
            _vehicleRepository.Update(vehicle);

            return ToDto(vehicle);
        }

        public VehicleDto GetByChassis(string chassisSeries, uint chassisNumber)
        {
            var vehicle = _vehicleRepository.GetByChassis(chassisSeries, chassisNumber);

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
            var vehicle = _vehicleRepository.GetByChassis(chassisSeries, chassisNumber);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            _vehicleRepository.Delete(vehicle);

            return ToDto(vehicle);
        }

        private static VehicleDto ToDto(Vehicle vehicle)
        {
            return new VehicleDto
            {
                ChassisSeries       = vehicle.ChassisSeries,
                ChassisNumber       = vehicle.ChassisNumber,
                Type                = vehicle.GetType().Name,
                Color               = vehicle.Color,
                NumberOfPassengers  = vehicle.NumberOfPassengers
            };
        }
    }
}
