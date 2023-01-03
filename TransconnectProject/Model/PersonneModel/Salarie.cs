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
        public double Salaire{get => this.salaire;set { this.salaire = value; }}
        public List<Salarie> Employ { get => this.employés; set { this.employés = value; } }
        public Poste Poste{ get => this.poste;set { this.poste = value; }}

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Salarie);
        }
        public bool Equals(Salarie obj)
        {
            if (obj != null)
            {
                if (this.Nom == obj.Nom && this.Prenom == obj.Prenom && this.Dob.ToString("d") == obj.Dob.ToString("d"))
                    return true;
                return false;
            }
            return false;
        }
        //TOTEST
        public int getEncienneteEnjours()
        {
            var today = DateTime.Now;
            return (today - this.dateArrive).Days;
        }

        public override string ToString()
        {
            return "["+this.poste.NomPoste + "] " + base.ToString();
        }




    }
}

