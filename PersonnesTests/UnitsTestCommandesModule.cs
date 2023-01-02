using System;
using CsvHelper;
using System.Globalization;
using TransconnectProject.Util;
using TransconnectProject.Controleur;
using TransconnectProject.Model;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Model.CommandeModel;
using TransconnectProject.Model.VehiculeModel;
using TransconnectProject.Model.ProduitModel;

namespace ProjectTests
{
    public class UnitsTestCommandesModule
    {
        private TransconnectControleur controleur;
        private Client client1;
        private Salarie chauffeur1;
        [SetUp]
        public void setup()
        {
            controleur = new TransconnectControleur(null);
            client1 = new Client("Messi", "Lionnel", new DateTime(2001, 03, 11), new Adresse("Marseille", "Los pequenos y pequenas"), "messi@hotmail.com", "0658497123");
            chauffeur1 = new Salarie("Vanackor", "Coco", new DateTime(2001, 03, 11), new Adresse("Paris", "6 rue jean bouins"), "JP@hotmail.com", "0765629493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
        }

        [Test]
        public void createNewCommandeTest()
        {
            #region Manuel part
            Produit choco = new Produit("chocolat", 3);
            Commande c1 = new Commande(client1, chauffeur1, new Voiture(5), new Produit("cacao", 2.5), 15, "Bordeaux");
            Commande c2 = new Commande(client1, chauffeur1, new Voiture(5), new Produit("chocolat", 3), 50, "Pau",dateDeLivraison:new DateTime(2023,03,20));
            //Commande c3 = new Commande(client1, chauffeur1, new Voiture(5), new Produit("chocolat", 3), 50, "Pau", dateDeLivraison: new DateTime(2003, 03, 20));
            //À ce jours
            /*c1.getTrajetLivraison(true);
            Console.WriteLine("prix => "+c1.Prix+" euros");
            Console.WriteLine();*/
            //Date choisit
            /*c2.getTrajetLivraison(true);
            Console.WriteLine("prix => " + c2.Prix + " euros");*/
            //date antérieur
            /*Console.WriteLine();
            c3.getTrajetLivraison(true);
            Console.WriteLine("prix => " + c3.Prix + " euros");*/


            //Check chez livreur
            Chauffeur c =(Chauffeur) chauffeur1.Poste;
            /*foreach(var item in c.ListeDeCommandes)
            {
                //item.getTrajetLivraison(true);
                Console.WriteLine(item.ToString());
            }*/
            #endregion
            #region via Client
            client1.doOrder("Paris", choco, 39,chauffeur1, new Voiture(5),dateLiv: new DateTime(2024, 03, 20));
            foreach (var item in client1.CommandesClient)
            {
                Console.WriteLine(item.ToString());
            }
            #endregion
        }

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

            //Indirect (console.write desactivé) 
            DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Paris", "Toulon", controleur.Ptw.CitiesList, true);
            DijkstraFeatures.Dijkstra(controleur.Ptw.PathMatrice, "Paris", "La Rochelle", controleur.Ptw.CitiesList, true);
        }
    }
}
