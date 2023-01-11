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
        /// <summary>
        /// Retourne le nombre de jours d'ancienenté
        /// </summary>
        /// <returns></returns>
        public int getEncienneteEnjours()
        {
            var today = DateTime.Now;
            return (today - this.dateArrive).Days;
        }

        public override string ToString()
        {
            return "["+this.poste.NomPoste + "] " + base.ToString();
        }
        /// <summary>
        /// This methode create a new salarie
        /// </summary>
        /// <returns></returns>
        public static Salarie createSalarie()
        {
            Console.WriteLine("\n- Fonctionnalite de creation de salarie -\n");
            Console.Write("veuillez saisir le nom du salarie: ");
            string nom = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir le Prenom du salarie: ");
            string prenom = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir la date de naissance du salarie (yyyy/mm/dd): ");
            string dob = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir la ville du salarie: ");
            string ville = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir la rue du salarie: ");
            string rue = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir l'email du salarie: ");
            string email = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir le numero de telephone du salarie: ");
            string numTel = Console.ReadLine();
            Poste pst =null;
            Console.Clear();
            do
            {
                Console.Write("\nveuillez saisir le nom du poste: ");
                string saisie = Console.ReadLine();
                pst = Poste.getPoste(saisie);
                if(pst==null)
                    Console.WriteLine("Erreur, saisie incorrect...\n");

            } while (pst == null);
            
            return new Salarie(nom, prenom, new DateTime(int.Parse(dob.Split("/")[0]), int.Parse(dob.Split("/")[1]), int.Parse(dob.Split("/")[2])), new Adresse(ville, rue), email, numTel,DateTime.Today,pst,new List<Salarie>());
        }
    }
}

