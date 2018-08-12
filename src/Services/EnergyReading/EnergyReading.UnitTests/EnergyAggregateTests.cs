using EnergyReading.Core.EnergyReadingAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnergyReading.UnitTests
{
    [TestClass]
    public class EnergyAggregateTests
    {

        [TestMethod]
        public void MedianValue()
        {
            var m = new List<IEnergyReading> { new TOUEnergyReading { Energy = 10 }, new LPEnergyReading { DataValue = 5 } }.Median(e => e.UsageValue);

            Assert.AreEqual(m, 7.5m);
        }

        [TestMethod]
        public void FindDivergentData()
        {
            var repo = MockRepository();
            var collections = repo.GroupByCollection();

            var touRecords = collections.Single(c => c.Name == TOURecordsName);
            Assert.IsNotNull(touRecords);

            // exclude 5 and 6 numbers
            var divData = touRecords.FindDivergentData(20);
            Assert.AreEqual(8, divData.Count());

            // i've excluded the correct numbers
            var foundExcludedNumbers = divData.Any(d => d.UsageValue == 5 || d.UsageValue == 6);

            Assert.IsFalse(foundExcludedNumbers);
        }

        const string TOURecordsName = "TOURecords";
        const string LPRecordsName = "LPRecords";

        private IEnergyReadingsRepository MockRepository()
        {
            var touRecords = new List<IEnergyReading>();

            for (var i = 1; i <= 10; i++)
                touRecords.Add(new TOUEnergyReading { DateTime = DateTime.UtcNow.AddHours(-i), Energy = i, CollectionId = "TOURecords" });

            var touCollection = new EnergyReadingsCollection
            {
                Name = "TOURecords",
                Records = touRecords
            };

            var repo = new Mock<IEnergyReadingsRepository>();

            repo.Setup(r => r.List()).Returns(touRecords);
            repo.Setup(r => r.GroupByCollection()).Returns(new List<EnergyReadingsCollection> { touCollection });
            return repo.Object;
        }
    }
}
