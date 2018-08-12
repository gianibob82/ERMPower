using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyReading.Core.EnergyReadingAggregate
{
    public sealed class TOUEnergyReading : EnergyReading, IEnergyReading
    {
        public decimal Energy { get; set; }

        public decimal UsageValue => Energy;
    }
}
