using System;
using System.Runtime.Serialization;
using TransconnectProject.Util;
using TransconnectProject.Model.CommandeModel;

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
        public double AchatCumulle { get => this.achatCumulle;}
        public List<Commande> CommandesClient { get => this.CommandesClient; set => this.commandes = value;}

        /// <summary>
        /// Effectuer une commande
        /// </summary>
        public void doOrder() { }
    }
}

