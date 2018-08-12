using CsvHelper;
using EnergyReading.Core.EnergyReadingAggregate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EnergyReading.Infrastructure.Data
{
    public class CsvEnergyReadingsRepository : IEnergyReadingsRepository
    {
        private readonly IEnumerable<IEnergyReading> _context;

        public CsvEnergyReadingsRepository(string pathToFolder)
        {
            _context = ReadFromFiles(pathToFolder);
        }

        public IEnumerable<IEnergyReading> List()
        {
            return _context;
        }

        public IEnumerable<IEnergyReading> GetByCollectionId(string collectionId)
        {
            return _context.ToList().Where(e => e.CollectionId == collectionId);
        }

        public IEnumerable<EnergyReadingsCollection> GroupByCollection()
        {
            return _context.GroupBy(g => g.CollectionId).Select(g => new EnergyReadingsCollection { Name = g.Key, Records = g.AsEnumerable() });
        }
        private IEnumerable<IEnergyReading> ReadFromFiles(string pathToFolder)
        {
            if (string.IsNullOrEmpty(pathToFolder))
                throw new ArgumentNullException("Path to folder is invalid");

            var files = Directory.GetFiles(pathToFolder, "*.csv");
            if (files.Count() <= 0)
                throw new FileNotFoundException("No csv files in specified folder");

            foreach (var f in files)
            {
                using (TextReader fileReader = File.OpenText(f))
                {
                    var csv = new CsvReader(fileReader);
                    csv.Read();
                    csv.ReadHeader();
                    csv.Configuration.HasHeaderRecord = false;

                    var collectionId = Path.GetFileNameWithoutExtension(f);

                    while (csv.Read())
                    {
                        if (collectionId.StartsWith("lp_", StringComparison.OrdinalIgnoreCase))
                        {
                            yield return CreateLPEnergyData(csv, collectionId);
                        }
                        else if (collectionId.StartsWith("tou_", StringComparison.OrdinalIgnoreCase))
                        {
                            yield return CreateTOUEnergyData(csv, collectionId);
                        }
                    }
                }
            }
        }

        private LPEnergyReading CreateLPEnergyData(CsvReader csv, string documentId)
        {
            return new LPEnergyReading
            {
                DateTime = csv.GetField<DateTime>(3),
                DataValue = csv.GetField<decimal>(5),
                CollectionId = documentId
            };
        }

        private TOUEnergyReading CreateTOUEnergyData(CsvReader csv, string documentId)
        {
            return new TOUEnergyReading
            {
                DateTime = csv.GetField<DateTime>(3),
                Energy = csv.GetField<decimal>(5),
                CollectionId = documentId
            };
        }


    }
}
