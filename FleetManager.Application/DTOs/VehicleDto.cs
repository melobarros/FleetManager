using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Application.DTOs
{
    public class VehicleDto
    {
        public string ChassisSeries { get; set; }
        public uint ChassisNumber { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int NumberOfPassengers { get; set; }
    }
}
