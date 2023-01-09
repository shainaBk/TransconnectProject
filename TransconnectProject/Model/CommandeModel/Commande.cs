using System;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Model;
using TransconnectProject.Util;
using TransconnectProject.Model.ProduitModel;
using TransconnectProject.Model.VehiculeModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
using Microsoft.VisualBasic;

namespace TransconnectProject.Model.CommandeModel
{
    public class Commande
	{
        //TODO: lier cette commande des produits
		private Produit produit;
		private int quantite;//en Kg
		private string proprietaireNom;
        private string proprietairePrenom;
        private Salarie chauffeurAffile;
		private Vehicule vehiculeAffile;
		private PathCityWritter ptw;
		private int distance;
		private double prix;
		private string villeA;
		private string villeB;
		private DateTime dateDeLivraison;

		//TOTEST
		public Commande(string clientNom,string clientPrenom,Salarie chauffeur,Vehicule vehicule,Produit produit,int quantity, string villeA, string villeB,DateTime? dateDeLivraison=null)
		{
            try
			{
                this.ptw = new PathCityWritter();
                this.proprietaireNom = clientNom;
                this.proprietairePrenom = clientPrenom;
                this.villeA = villeA;
                this.villeB = villeB;
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
                this.vehiculeAffile = vehicule;
				this.chauffeurAffile = chauffeur;
				this.distance = DijkstraFeatures.Dijkstra(ptw.PathMatrice, this.villeA, this.villeB, ptw.CitiesList);
				if (this.distance == -1) throw new Exception("Erreur, nous ne pouvons effectuer de course entre ces deux villes");
				this.prix = (this.produit.PrixKg * quantite) + (this.distance * 0.5) + (Chauffeur.getTarif() + chauffeurAffile.getEncienneteEnjours() * 0.015); //+ this.vehiculeAffile.PrixLocation; //+ (distance * 0.5) + (Chauffeur.getTarif() + chauffeurAffile.getEncienneteEnjours() * 0.015);//0,5 euro par km

            }
            catch(Exception e)
			{
				Console.WriteLine("Objet non cree car: "+e);
			}
        }
		public string VilleA { get => this.villeA; set => this.villeA = value; }
        public string VilleB { get => this.villeB; set => this.villeB = value; }
		public int Distance { get => this.distance; set => this.distance = value; }
		public Vehicule VehiculeAffile { get => this.vehiculeAffile; set => this.vehiculeAffile = value; }
		public int Quantite { get => this.quantite; set => this.quantite = value; }
		public Produit Produit { get => this.produit; set => this.produit = value; }
        public string ProprietaireNom { get => this.proprietaireNom; set => this.proprietaireNom = value; }
        public string ProprietairePrenom { get => this.proprietairePrenom; set => this.proprietairePrenom = value; }
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
            return "\nCommande de "+this.produit.NomProduit+""+"\n- quantité: "+this.quantite+"Kg\n"+"- Propriétaire: "+this.proprietairePrenom+" "+this.proprietaireNom+"\n- À livré le "+this.dateDeLivraison.ToString("d")+"\n- Info Points livraison: "+ptw.CurrentPath+"\n- Livreur: "+chauffeurAffile.ToString()+"\n- Vehicule info: "+this.vehiculeAffile.ToString()+"\nPrix total = "+this.Prix+" euro\n\n";
        }
    }
}

