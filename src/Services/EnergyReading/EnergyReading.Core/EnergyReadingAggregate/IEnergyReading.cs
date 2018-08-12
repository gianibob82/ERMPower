using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyReading.Core.EnergyReadingAggregate
{
    public interface IEnergyReading
    {
        DateTime DateTime { get; }
        decimal UsageValue { get; }
        string CollectionId { get; }
    }
}
