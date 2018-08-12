using EnergyReading.Core.EnergyReadingAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyReading.API.Application
{
    public class EnergyReadingQueries : IEnergyReadingQueries
    {
        readonly IEnergyReadingsRepository _repo;
        public EnergyReadingQueries(IEnergyReadingsRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<EnergyReadingGroup> FindDivergingReadings(byte percValue)
        {
            var groups = _repo.GroupByCollection();
            foreach (var g in groups)
            {
                yield return new EnergyReadingGroup {
                     name = g.Name,
                      medianvalue = g.MedianValue(),
                       readings = g.FindDivergentData(percValue).Select(r => new EnergyReading { date = r.DateTime, value = r.UsageValue })
                };
            }

        }
    }
}
