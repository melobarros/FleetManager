using FleetManager.Domain.Entities.Diagnostics;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Interfaces;
using FleetManager.Infrastructure.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.EntityFramework.Repositories
{
    public class ProtocolRepository : IProtocolRepository
    {
        private readonly AppDbContext _dbContext;

        public ProtocolRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DiagnosticProtocol? GetByVehicleType(VehicleType vehicleType)
        {
            return _dbContext.DiagnosticProtocols
                             .Include(p => p.Sensors)
                             .Include(p => p.ErrorCodes)
                             .FirstOrDefault(p => p.VehicleType == vehicleType);
        }
    }
}
