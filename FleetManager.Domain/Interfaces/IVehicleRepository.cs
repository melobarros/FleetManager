using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        Vehicle? GetByChassis(string chassisSeries, uint chassisNumber);
        IEnumerable<Vehicle> GetAll();
        void Add(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(Vehicle vehicle);
    }
}
