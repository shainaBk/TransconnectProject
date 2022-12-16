using System;
using TransconnectProject.Util;

namespace TransconnectProject.Model
{
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

