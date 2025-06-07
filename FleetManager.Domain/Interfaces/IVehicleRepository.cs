using FleetManager.Domain.Entities;
using FleetManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        Vehicle? GetByChassis(ChassisId chassisId);
        IEnumerable<Vehicle> GetAll();
        void Add(Vehicle vehicle);
        void Update(Vehicle vehicle);
    }
}
