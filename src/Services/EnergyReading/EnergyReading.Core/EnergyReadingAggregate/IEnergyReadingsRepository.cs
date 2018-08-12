using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyReading.Core.EnergyReadingAggregate
{
    public interface IEnergyReadingsRepository
    {
        IEnumerable<IEnergyReading> List();

        IEnumerable<EnergyReadingsCollection> GroupByCollection();
    }
}
