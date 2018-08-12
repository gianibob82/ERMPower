using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyReading.Core.EnergyReadingAggregate
{
    public sealed class LPEnergyReading : EnergyReading, IEnergyReading
    {
        public decimal DataValue { get; set; }

        public decimal UsageValue => DataValue;

    }
}
