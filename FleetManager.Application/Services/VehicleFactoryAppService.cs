using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Factories;
using FleetManager.Domain.Interfaces;

namespace FleetManager.Application.Services
{
    public class VehicleFactoryAppService : IVehicleFactoryAppService
    {
        private readonly IProtocolRepository _protocolRepository;

        public VehicleFactoryAppService(IProtocolRepository protocolRepository)
        {
            _protocolRepository = protocolRepository;
        }

        public Vehicle Create(VehicleType type, string chassisSeries, uint chassisNumber, string color)
        {
            var vehicle = VehicleFactory.Create(type, chassisSeries, chassisNumber, color);
            var protocol = _protocolRepository.GetByVehicleType(type);

            if (protocol == null)
                throw new InvalidOperationException("Diagnostic protocol not found for vehicle type");

            vehicle.SetDiagnosticProtocol(protocol);
            return vehicle;
        }
    }
}