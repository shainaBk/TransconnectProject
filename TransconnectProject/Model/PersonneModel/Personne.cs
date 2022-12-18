using System;
using System.Runtime.Serialization;
using TransconnectProject.Util;
namespace TransconnectProject.Model
{
    [DataContract]
    public abstract class Personne
	{
        [DataMember] private string nom;
        [DataMember] private string prenom;
        [DataMember] private DateTime dob;//Date de naissance
        [DataMember] private Adresse adressePostal;
        [DataMember] private string mail;//(otional) add validator method
        [DataMember] private string telNum;//(optional) add validator method

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

