using System;
using System.Runtime.Serialization;
using TransconnectProject.Util;
using TransconnectProject.Model.CommandeModel;
using TransconnectProject.Model.ProduitModel;
using TransconnectProject.Model.VehiculeModel;

namespace TransconnectProject.Model
{
    [DataContract]
    public class Client : Personne
    {
        [DataMember] private List<Commande> commandes; //liste de commandes client
        [DataMember] private double achatCumulle;//achat cummulés client

        public Client(string nom, string prenom, DateTime dob, Adresse adressePostal, string mail, string telNum) : base(nom, prenom, dob, adressePostal, mail, telNum)
        {
            this.commandes = new List<Commande>();
            this.achatCumulle = 0;
        }
        public string Ville { get => this.adressePostal.Ville; }
        public double AchatCumulle { get => this.achatCumulle;}
        public List<Commande> CommandesClient { get => this.commandes; set => this.commandes = value;}

        /// <summary>
        /// Effectuer une commande
        /// </summary>
        //TOTEST
        public void doOrder(string from,Produit produit, int quantite,Salarie chauffeur,Vehicule vehicule,DateTime?dateLiv=null) {
            this.commandes.Add(new Commande(this,chauffeur,vehicule,produit,quantite, from,dateDeLivraison:dateLiv));
        }
    }
}

