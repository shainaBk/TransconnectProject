using System;
using TransconnectProject.Util;
using TransconnectProject.Model.PosteModel;
using System.Runtime.Serialization;

namespace TransconnectProject.Model
{
    [DataContract]
    public class Salarie : Personne
    {
        public static int num = 0;
        [DataMember] private int numSS;
        [DataMember] private double salaire;//dépend du poste
        [DataMember] private DateTime dateArrive;
        [DataMember] private List<Salarie> employés;//Liste des employés du salarié
        [DataMember] private Poste poste;

        public Salarie(string nom, string prenom, DateTime dob, Adresse adressePostal, string mail, string telNum, DateTime dateArrive, Poste poste, List<Salarie>employés) : base(nom, prenom, dob, adressePostal, mail, telNum)
        {
            num++;
            this.numSS = numSS + num;
            this.dateArrive = dateArrive;
            this.poste = poste;
            this.salaire = this.poste.Salaire;
            this.employés = employés;
            
        }

        public List<Salarie> Employ { get => this.employés; set { this.employés = value; } }

         public Poste Poste{
            get=> this.poste;
            set{this.poste=value;}
        }
         
        public double Salaire
        {
            get => this.salaire;
            set { this.salaire = value; }
        }
    }
}

