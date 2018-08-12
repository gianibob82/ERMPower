using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyReading.API.Application
{
    public class EnergyReadingGroup
    {
        public EnergyReadingGroup()
        {
            readings = new List<EnergyReading>();
        }
        public string name { get; set; }
        public decimal medianvalue { get; set; }

        public IEnumerable<EnergyReading> readings { get; set; }
    }

    public class EnergyReading
    {
        public DateTime date { get; set; }
        public decimal value { get; set; }
    }
}
