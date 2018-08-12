using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyReading.Core.EnergyReadingAggregate
{
    public abstract class EnergyReading
    {
        public DateTime DateTime { get; set; }
        public string CollectionId { get; set; }
    }
}
