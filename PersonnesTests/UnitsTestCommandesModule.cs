using System;
using CsvHelper;
using System.Globalization;
using TransconnectProject.Util;
namespace ProjectTests
{
    public class UnitsTestCommandesModule
    {
        [SetUp]
        public void setup()
        {
        }

        //TODO: continue
        [Test]
        public void Dijstrafeaturetest()
        {
            using (var reader = new StreamReader("../../../../TransconnectProject/serializationFiles/Distances.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PathCityWriter>();
                Console.WriteLine("there "+records.ElementAt(0).CityA);
            }

        }
    }
}
