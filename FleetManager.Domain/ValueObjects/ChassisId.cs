using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Domain.ValueObjects
{
    public class ChassisId
    {
        public ChassisId() { }

        public string Series { get; set; }
        public uint Number { get; set; }

        public ChassisId(string series, uint number)
        {
            Series = series;
            Number = number;
        }
    }
}
