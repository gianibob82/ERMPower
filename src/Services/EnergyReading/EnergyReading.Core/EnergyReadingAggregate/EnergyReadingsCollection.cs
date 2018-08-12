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

        public decimal MedianValue()
        {
            return Records.Select(r => r.UsageValue).Median();
        }

        public IEnumerable<IEnergyReading> FindDivergentData(byte percVariance)
        {
            if (percVariance < 1 || percVariance > 100)
                throw new ArgumentOutOfRangeException("Values 1 to 100 only.");

            var median = MedianValue();

            decimal percentage = 1M + (percVariance / 100M);

            var topMedian = median * percentage;
            var lowMedian = median / percentage;

            return Records.Where(r => r.UsageValue > topMedian || r.UsageValue < lowMedian);
        }

        public StringBuilder PrintDivergentData(byte percVariance)
        {
            var sb = new StringBuilder();
            var median = MedianValue();

            foreach (var d in FindDivergentData(percVariance))
            {
                sb.AppendLine($"{this.Name} {d.DateTime} {d.UsageValue} {median}");
            }

            return sb;
        }
    }
}
