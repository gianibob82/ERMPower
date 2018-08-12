using EnergyReading.Core.EnergyReadingAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnergyReading.UnitTests
{
    [TestClass]
    public class CsvEnergyReadingsRepositoryTests
    {
        [TestMethod]
        public void GroupByCollection()
        {
            IEnergyReadingsRepository repo = new FakeEnergyReadingsRepository();
            var collections = repo.GroupByCollection();

            Assert.AreEqual(collections.Count(), 2);

            Assert.IsTrue(collections.All(c => c.Records.Count() == 10));
        }
    }

    public class FakeEnergyReadingsRepository : IEnergyReadingsRepository
    {

        public IEnumerable<IEnergyReading> GetByCollectionId(string collectionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EnergyReadingsCollection> GroupByCollection()
        {
            return ReturnItems().GroupBy(g => g.CollectionId).Select(g => new EnergyReadingsCollection { Name = g.Key, Records = g.AsEnumerable() });
        }

        public IEnumerable<IEnergyReading> List()
        {
            throw new NotImplementedException();
        }


        private IEnumerable<IEnergyReading> ReturnItems()
        {
            for (var i = 1; i <= 10; i++)
                yield return new TOUEnergyReading { DateTime = DateTime.UtcNow.AddHours(-i), Energy = i, CollectionId = "TOURecords" };

            for (var i = 1; i <= 10; i++)
                yield return new LPEnergyReading { DateTime = DateTime.UtcNow.AddHours(-i), DataValue = i, CollectionId = "LPRecords" };

        }
    }
}
