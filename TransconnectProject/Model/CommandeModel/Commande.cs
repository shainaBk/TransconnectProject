using System;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Model;
using TransconnectProject.Util;
using TransconnectProject.Model.ProduitModel;
using TransconnectProject.Model.VehiculeModel;
namespace TransconnectProject.Model.CommandeModel
{
	public class Commande
	{
		//TODO: lier cette commande des produits
		private Produit produit;
		private int quantite;//en Kg
		private Client ClientProprietaire;
		private Salarie chauffeurAffile;
		private Vehicule vehiculeAffile;
		private PathCityWritter ptw;
		private int distance;
		private double prix;
		private string villeA;
		private string villeB;
		private DateTime dateDeLivraison;

		//TOTEST
		public Commande(Client client,Salarie chauffeur,Vehicule vehicule,Produit produit,int quantity, string villeA, string villeB = null,DateTime? dateDeLivraison=null)
		{
            this.ptw = new PathCityWritter();
            try
			{
                
                this.villeA = villeA;
                if (villeB != null)
                    this.villeB = villeB;
                else
                    this.villeB = client.Ville;
				if (!ptw.CitiesList.Contains(this.villeA) || !ptw.CitiesList.Contains(this.villeB))
					throw new Exception("Erreur, l'une des villes saisie n'existe pas");
				/*********** Dijkstra PART **********/
				DijkstraFeatures.Dijkstra(ptw.PathMatrice, this.villeA, this.villeB, ptw.CitiesList, ptw);
				/*************************/
                if (!dateDeLivraison.HasValue)
					this.dateDeLivraison = DateTime.Now;
				else
				{
					if (DateTime.Compare((DateTime)dateDeLivraison, DateTime.Now) > 0 || DateTime.Compare((DateTime)dateDeLivraison, DateTime.Now) == 0)
						this.dateDeLivraison = (DateTime)dateDeLivraison;
					else throw new Exception("Erreur, vous ne pouvez effectuer une commande pour une date anterieur à celle actuelle");
				}
				this.produit = produit;
				this.quantite = quantity;
				
				this.ClientProprietaire = client;
                this.vehiculeAffile = vehicule;
                //usefull for test, or specific delevery city
               

                if (chauffeur.Poste is Chauffeur)
                    this.chauffeurAffile = chauffeur;
                else throw new Exception("Erreur, le salarie choisis n'est pas un chauffeur ! Bye !");
                
				this.distance = DijkstraFeatures.Dijkstra(ptw.PathMatrice, this.villeA, this.villeB, ptw.CitiesList);
				if (this.distance == -1) throw new Exception("Erreur, nous ne pouvons effectuer de course entre ces deux villes");

				this.prix = (this.produit.PrixKg * quantite) + this.vehiculeAffile.PrixLocation + (distance * 0.5) + (Chauffeur.getTarif() + chauffeurAffile.getEncienneteEnjours() * 0.015);//0,5 euro par km                                                                                                                                                                   //TOTEST
                ((Chauffeur)this.chauffeurAffile.Poste).addCommande(this);
			}catch(Exception e)
			{
				Console.WriteLine("Objet non cree car: "+e);
			}
        }
		public Client ClientCom { get => this.ClientProprietaire; set => this.ClientProprietaire = value; }
        public Salarie ChauffeurCom { get => this.chauffeurAffile; set => this.chauffeurAffile = value; }
        public DateTime DateDeLivraison { get => this.dateDeLivraison; set => this.dateDeLivraison = value; }
		public double Prix { get => this.prix; }
		//Show path for the order
		public void getTrajetLivraison()
		{
			Console.WriteLine(this.ptw.CurrentPath);
        }
        public override string ToString()
        {
            return "\nCommande de "+this.produit.NomProduit+", quantité: "+this.quantite+"Kg\n- À livré le "+this.dateDeLivraison.ToString("d")+"\n- Info Points livraison: "+ptw.CurrentPath+"\n- Livreur: "+chauffeurAffile.ToString()+"\n- Prix total = "+this.Prix+" euro\n\n";
        }
    }
}

