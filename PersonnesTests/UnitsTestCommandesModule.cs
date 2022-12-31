using System;
using CsvHelper;
using System.Globalization;
using TransconnectProject.Util;
using TransconnectProject.Controleur;
namespace ProjectTests
{
    public class UnitsTestCommandesModule
    {
        private TransconnectControleur controleur;
        [SetUp]
        public void setup()
        {
            controleur = new TransconnectControleur(null);
        }

        //TODO: continue
        [Test]
        public void DijstrafeatureTest()
        {
            //Direct
            var km = DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Paris", "Marseille", controleur.Ptw.CitiesList, true);
            Assert.AreEqual(777, km);
            km = DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Marseille", "Paris", controleur.Ptw.CitiesList, true);
            Assert.AreEqual(777, km);

            km = DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Paris", "Pau", controleur.Ptw.CitiesList, true);
            Assert.AreEqual(791, km);
            km = DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Pau", "Paris", controleur.Ptw.CitiesList, true);
            Assert.AreEqual(791, km);

            km = DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Toulon", "Monaco", controleur.Ptw.CitiesList, true);
            Assert.AreEqual(169, km);
            km = DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Monaco", "Toulon", controleur.Ptw.CitiesList, true);
            Assert.AreEqual(169, km);

            km = DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Biarritz", "Toulouse", controleur.Ptw.CitiesList, true);
            Assert.AreEqual(309, km);
            km = DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Biarritz", "Toulouse", controleur.Ptw.CitiesList, true);
            Assert.AreEqual(309, km);

            //Indirect
            DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Paris", "Toulon", controleur.Ptw.CitiesList, true);
            DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Paris", "La Rochelle", controleur.Ptw.CitiesList, true);
        }
    }
}
