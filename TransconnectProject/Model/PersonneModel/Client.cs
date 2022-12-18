using System;
using System.Runtime.Serialization;
using TransconnectProject.Util;

namespace TransconnectProject.Model
{
    [DataContract]
    public class Client : Personne
    {
        //private List<Commande> commandes //liste de commandes client
        private double achatCumulle;//achat cummulés client
        public Client(string nom, string prenom, DateTime dob, Adresse adressePostal, string mail, string telNum) : base(nom, prenom, dob, adressePostal, mail, telNum)
        {
            this.achatCumulle = 0;
        }

        public void doOrder() { }//Effectuer une commande
    }
}

