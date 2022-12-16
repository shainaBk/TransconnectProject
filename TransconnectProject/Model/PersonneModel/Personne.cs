using System;
using TransconnectProject.Util;
namespace TransconnectProject.Model
{
	public abstract class Personne
	{
		private string nom;
		private string prenom;
		private DateTime dob;//Date de naissance
		private Adresse adressePostal;
		private string mail;//(otional) add validator method
		private string telNum;//(optional) add validator method

        protected Personne(string nom, string prenom, DateTime dob, Adresse adressePostal, string mail, string telNum)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.dob = dob;
            this.adressePostal = adressePostal;
            this.mail = mail;
            this.telNum = telNum;
        }

        public string Nom{ get=> this.nom; set { this.nom = value; } }
        public string Prenom{ get=> this.prenom; set { this.prenom = value; } }
        public string TelNum{ get=> this.telNum; set { this.telNum = value; } }
        public DateTime Dob { get=>this.dob.Date; set { this.dob = value; } }

    }
}

