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
			try
			{
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
				this.villeA = villeA;
				this.ClientProprietaire = client;
				//usefull for test, or specific delevery city
				if (villeB != null)
					this.villeB = villeB;
				else
					this.villeB = ClientProprietaire.Ville;
				this.vehiculeAffile = vehicule;
                if (chauffeur.Poste is Chauffeur)
                    this.chauffeurAffile = chauffeur;
                else throw new Exception("ERROR: le salarie choisis n'est pas un chauffeur ! Bye !");
                
				this.ptw = new PathCityWritter();
				//TODO:AddException si ville client existe pas!
				//TODO: Mettre en place liste de ville accessible
				this.distance = DijkstraFeatures.Dijkstra(ptw.PathMatrice, this.villeA, this.villeB, ptw.CitiesList, false);
				this.prix = (this.produit.PrixKg * quantite) + this.vehiculeAffile.PrixLocation + (distance * 0.5) + (Chauffeur.getTarif() + chauffeurAffile.getEncienneteEnjours() * 0.015);//0,5 euro par km                                                                                                                                                                   //TOTEST
                ((Chauffeur)this.chauffeurAffile.Poste).addCommande(this);
			}catch(Exception e)
			{
				Console.WriteLine("Objet non créé car: "+e);
			}
        }
		public DateTime DateDeLivraison { get => this.dateDeLivraison; set => this.dateDeLivraison = value; }
		public double Prix { get => this.prix; }
		//Show path for the order
		public void getTrajetLivraison(bool testMode)
		{
            DijkstraFeatures.Dijkstra(ptw.PathMatrice, this.villeA, this.villeB, ptw.CitiesList, testMode,this.ptw);
        }
		//TOTEST
        public override string ToString()
        {
			getTrajetLivraison(true);
            return "Commande de "+this.produit.NomProduit+", quantité: "+this.quantite+"Kg\n- À livré le "+this.dateDeLivraison.ToString("d")+"\n- Info Points livraison: "+ptw.CurrentPath+"\n- Livreur: "+chauffeurAffile.ToString()+"\n- Prix total = "+this.Prix+" euro\n\n";
        }
    }
}

