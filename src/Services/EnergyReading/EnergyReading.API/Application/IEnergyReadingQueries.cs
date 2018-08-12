using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyReading.API.Application
{
    public interface IEnergyReadingQueries
    {
        /// <summary>
        /// Find records that are above or below each group median value
        /// </summary>
        /// <param name="percValue">Divergence expressed with % value</param>
        /// <returns></returns>
        IEnumerable<EnergyReadingGroup> FindDivergingReadings(byte percValue); 
    }
}
