using System;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Model;
namespace TransconnectProject.Model.CommandeModel
{
	public class Commande
	{
		//TODO: lier cette commande des produits
		private Client ClientProprietaire;
		private Chauffeur chauffeurAffilé;
		private int prix;
		private string villeA;
		private string villeB;
		private DateTime dateDeLivraison;

		public Commande(Client client,string villeA,string villeB,Chauffeur chauffeur)
		{
			this.ClientProprietaire = client;
			this.villeA = villeA;
			this.villeB = villeB;//Might be the client city;
			this.chauffeurAffilé = chauffeur;
            //this.prix =  parcourt+véhicule+produit+tarifChauffeur
        }
    }
}

