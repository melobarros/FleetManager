using FleetManager.Domain.Entities;
using FleetManager.Domain.Interfaces;
using FleetManager.Infrastructure.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.EntityFramework.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _dbContext;

        public VehicleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Vehicle? GetByChassis(string chassisSeries, uint chassisNumber)
        {
            return _dbContext.Vehicles
                    .AsNoTracking()
                    .FirstOrDefault(v =>  
                        v.ChassisSeries == chassisSeries &&
                        v.ChassisNumber == chassisNumber);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return _dbContext.Vehicles.AsNoTracking().ToList();
        }

        public void Add(Vehicle vehicle)
        {
            _dbContext.Vehicles.Add(vehicle);
            _dbContext.SaveChanges();
        }

        public void Update(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
            _dbContext.SaveChanges();
        }

        public void Delete(Vehicle vehicle)
        {
            _dbContext.Vehicles.Remove(vehicle);
            _dbContext.SaveChanges();
        }
    }
}
