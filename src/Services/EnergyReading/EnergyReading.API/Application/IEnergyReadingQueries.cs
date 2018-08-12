using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyReading.API.Application
{
    public interface IEnergyReadingQueries
    {
        IEnumerable<EnergyReadingGroup> FindDivergingReadings(byte percValue); 
    }
}
