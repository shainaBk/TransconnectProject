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
        public Adresse Adresse { get => this.adressePostal; set => this.adressePostal = value; }
        public double AchatCumulle { get => this.achatCumulle;}
        public List<Commande> CommandesClient { get => this.commandes; set => this.commandes = value;}

        /// <summary>
        /// Effectuer une commande
        /// </summary>
        //TOTEST
        public void doOrder(string from,Produit produit, int quantite,Salarie chauffeur,Vehicule vehicule,DateTime?dateLiv=null) {
            Commande c = new Commande(this, chauffeur, vehicule, produit, quantite, from, dateDeLivraison: dateLiv);
            this.commandes.Add(c);
            this.achatCumulle += c.Prix;
        }
        public void doOrder(Commande c)
        {
            this.commandes.Add(c);
            this.achatCumulle += c.Prix;
        }
        //avarege commande
        public double getAverageCommande()
        {
            return achatCumulle / this.commandes.Count();
        }
        public string getListCommande()
        {
            string liste = null;
            foreach (var item in this.commandes)
            {
                liste += item.ToString();
            }
            if (liste != null)
                return "\n"+liste;
            else
                return "\nPas de commande...";
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Client);
        }
        public bool Equals(Client obj)
        {
            if (obj != null)
            {
                if (this.Nom == obj.Nom && this.Prenom == obj.Prenom && this.Dob.ToString("d") == obj.Dob.ToString("d"))
                    return true;
                return false;
            }
            return false;
        }

        //TODO: créer un client
        public static Client createClient()
        {
            Console.WriteLine("\n- Fonctionnalite de creation de client -\n");
            Console.Write("veuillez saisir le nom du client: ");
            string nom = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir le Prenom du client: ");
            string prenom =Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir la date de naissance du client (yyyy/mm/dd): ");
            string dob = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir la ville du client: ");
            //TODO:ADD VILLE CHECKER
            string ville = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir la rue du client: ");
            string rue = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir l'email du client: ");
            string email = Console.ReadLine();
            Console.WriteLine();
            Console.Write("veuillez saisir le numero de telephone du client: ");
            string numTel = Console.ReadLine();

            return new Client(nom,prenom,new DateTime(int.Parse(dob.Split("/")[0]), int.Parse(dob.Split("/")[1]), int.Parse(dob.Split("/")[2])),new Adresse(ville,rue),email,numTel);
        }

        public override string ToString()
        {
            return "Client: "+base.ToString()+"\nInfo adresse: "+this.adressePostal.ToString()+"\nMontant Achats cumullés = "+this.achatCumulle+" Euros\n";
        }
    }
}

