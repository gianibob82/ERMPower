using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnergyReading.Core.EnergyReadingAggregate
{
    public class EnergyReadingsCollection
    {
        public string Name { get; set; }
        public IEnumerable<IEnergyReading> Records { get; set; }

        /// <summary>
        /// Find the median value in this group
        /// </summary>
        /// <returns></returns>
        public decimal MedianValue()
        {
            return Records.Select(r => r.UsageValue).Median();
        }

        /// <summary>
        /// Find records that are above or below this group median value
        /// </summary>
        /// <param name="percVariance">Divergence expressed with % value</param>
        /// <returns></returns>
        public IEnumerable<IEnergyReading> FindMedianDivergentData(byte percVariance)
        {
            if (percVariance < 1 || percVariance > 100)
                throw new ArgumentOutOfRangeException("Values 1 to 100 only.");

            var median = MedianValue();

            decimal percentage = 1M + (percVariance / 100M);

            var topMedian = median * percentage;
            var lowMedian = median / percentage;

            return Records.Where(r => r.UsageValue > topMedian || r.UsageValue < lowMedian);
        }
    }
}
