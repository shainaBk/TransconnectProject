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
                //RECUPERATION TOUT LES CHEMINS
                var records = csv.GetRecords<PathCity>();
                //Console.WriteLine("there " + records.ElementAt(0).CityA);
                var recordsList = records.ToList();
                /*Console.WriteLine(recordsList.Count());
                Console.WriteLine("there2 " + recordsList[0].CityA);
                Console.WriteLine("there3 " + recordsList[recordsList.Count()-1].CityA);*/

                //ADD TOUTES LES VILLES
                HashSet<string> hashListePtsA = new HashSet<string>();
                List<string> listePtsA = new List<string>();
                foreach(var item in recordsList)
                {
                    hashListePtsA.Add(item.CityA);
                    hashListePtsA.Add(item.CityB);
                }
                //A
                //Console.WriteLine(listePtsA.Count());
                foreach (var item in hashListePtsA)
                {
                  //  Console.WriteLine(item);
                }
                int size = hashListePtsA.Count();
                //Console.WriteLine(hashListePtsA.Count());

                //CREATE MATRICE
                int[,] matrice = new int[size,size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (hashListePtsA.ElementAt(i) == hashListePtsA.ElementAt(j))
                            matrice[i, j] = 0;
                        else
                        {
                            var finder = recordsList.Find(x => x.CityA.Equals(hashListePtsA.ElementAt(i)) && x.CityB.Equals(hashListePtsA.ElementAt(j)));

                            matrice[i, j] = finder != null ? finder.Distance : int.MaxValue;
                        }
                    }
                }

                /* for (int i = 0; i < size; i++)
                 {
                     for (int j = 0; j < size; j++)
                     {
                         Console.Write(hashListePtsA.ElementAt(i)+" => " + matrice[i,j] +" => "+hashListePtsA.ElementAt(j)+" -- ");
                     }
                     Console.WriteLine();
                 }*/

                //TEST DISKRA

                DijkstraFeatures.Dijkstra(matrice, "Paris", "Bordeaux", hashListePtsA,true);
            }

        }
    }
}
