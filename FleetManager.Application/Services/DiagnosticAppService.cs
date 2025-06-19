using FleetManager.Application.DTOs;
using FleetManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Application.Services
{
    public class DiagnosticAppService : IDiagnosticAppService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public DiagnosticAppService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public DiagnosticResultDto RunDiagnostic(string chassisSeries, uint chassisNumber)
        {
            var vehicle = _vehicleRepository.GetByChassis(chassisSeries, chassisNumber);
            if(vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            var protocol = vehicle.DiagnosticProtocol;
            if (protocol == null)
                throw new InvalidOperationException("Diagnostic protocol not assigned to vehicle.");

            var result = new DiagnosticResultDto
            {
                VehicleType = protocol.VehicleType,
                ExecutionDate = DateTime.Now
            };

            var rnd = new Random();

            foreach (var sensor in protocol.Sensors)
            {
                int min = sensor.MinThreshold;
                int max = sensor.MaxThreshold;
                int value = rnd.Next(min, max + (max * 20 / 100));

                result.Readings.Add(new SensorReading(sensor.Name, value));

                if (value < min || value > max)
                {
                    var error = sensor.ErrorCode;
                    if (error != null)
                    {
                        result.Errors.Add(new DetectedError(error.Code, error.Description));
                    }
                }
            }

            return result;
        }
    }
}
