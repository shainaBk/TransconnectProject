using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransconnectProject.Model;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Util;
using TransconnectProject.Controleur.CritereClients;
using TransconnectProject.Model.ProduitModel;
using TransconnectProject.Model.VehiculeModel;
using TransconnectProject.Model.CommandeModel;

namespace TransconnectProject.Controleur
{

    public class TransconnectControleur
    {
        private List<Produit> listeDesProduits;
        //TOKNOW:MAY HAVE PB DE SERIALISTATION
        private List<Vehicule> ListeVehiculeDisponible; 
        private List<Salarie> salaries;
        private List<Client> clients;
        private List<Commande> commandes;
        private SalarieTree organigramme;
        //TODO: Implanter methode Dijskra
        private PathCityWritter ptw;//dijskra tools

        public TransconnectControleur(List<Salarie> salaries,List<Client>?clients=null, List<Produit>? Produits = null, List<Vehicule>? vehicules= null)
        {
            if (vehicules != null)
                this.ListeVehiculeDisponible= vehicules;
            else
                this.ListeVehiculeDisponible = new List<Vehicule>();
            if (Produits != null)
                this.listeDesProduits = Produits;
            else
                this.listeDesProduits = new List<Produit>();
            if (clients != null)
                this.clients = clients;
            else
                this.clients = new List<Client>();
            this.commandes = new List<Commande>();
            this.salaries = salaries;
            this.organigramme = new SalarieTree(new SalarieNode(null));
            ptw = new PathCityWritter();
            //BuildSalariesTree();//ATENTION POUR LE BIEN DE CERTAINS TESTS UNITS RETIRER
        }
        public List<Salarie> Salaries { get => this.salaries; set => this.salaries = value; }
        public List<Client> Clients { get => this.clients; set => this.clients = value; }
        public SalarieTree Organigramme { get => this.organigramme; set => this.organigramme = value; }
        public PathCityWritter Ptw { get => this.ptw;}
        public List<Produit> ListeDesProduits { get => this.listeDesProduits; set => this.listeDesProduits = value; }
        public List<Vehicule> ListeDesVehicules { get => this.ListeVehiculeDisponible; set => this.ListeVehiculeDisponible= value; }
        #region Salaries
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        public void deleteSalarie(string nom, string prenom)
        {
            var toDelete = this.salaries.Find(x => x.Nom.Equals(nom) && x.Prenom.Equals(prenom));
            if (toDelete != null)
            {
                //Pour liste controleur
                List<Salarie> Dep = this.salaries.FindAll(x => x.Poste.NomPoste != "Directeur general" && x.Poste.Departement.NomDep.Equals(toDelete.Poste.Departement.NomDep) && x.Poste.getNumHierarchique() < toDelete.Poste.getNumHierarchique());
                //pour organigramme
                List<Salarie> DepViaCeo = this.salaries.Find((x) => x.Poste.NomPoste == "Directeur general").Employ.Find(x => x.Poste.Departement.NomDep.Equals(toDelete.Poste.Departement.NomDep)).Employ;
                //CEO PART;
                List<string> listeDic = new List<string> { "Directeur commercial", "Directeur des opérations", "Directeur financier", "Directeur RH" };
                if (listeDic.Contains(toDelete.Poste.NomPoste))
                {
                    this.salaries.Find(x => x.Poste.NomPoste == "Directeur general").Employ.Remove(toDelete);
                }
                else
                {
                    //NORMAL EMPLOYÉS PART

                    if (Dep != null)
                    {
                        foreach (var item in Dep)
                        {
                            item.Employ.Remove(toDelete);
                        }
                    }
                    if (DepViaCeo != null)
                    {
                        foreach (var item in DepViaCeo)
                        {
                            if (item.Employ.Contains(toDelete))
                                item.Employ.Remove(toDelete);
                        }
                    }

                }
                this.salaries.Remove(toDelete);

                //JSON PART
                    try
                    {
                        JsonUtil.sendJsonSalaries(this.salaries);
                        Console.WriteLine("Salarié supprimé de la base de donnée !");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error push into JSON file, message => " + e);
                    }

                //refresh organnigramme
                BuildSalariesTree();
            }
            else
                Console.WriteLine("Le salarie "+nom+" "+prenom+" n'est pas dans notre base de donnée");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public void addSalarie(Salarie s,string nomSuperieur, string prenomSuperieur)
        {
            if (!this.salaries.Contains(s))
            {
                List<string> listeDic = new List<string> { "Directeur commercial", "Directeur des operations", "Directeur financier", "Directeur RH" };
                //pour organigramme
                List<Salarie> DepViaCeo = this.salaries.Find((x) => x.Poste.NomPoste == "Directeur general").Employ.Find(x=>x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep)).Employ;
                //Pour liste controleur
                List<Salarie> Dep = this.salaries.FindAll((x) => x.Poste.NomPoste!= "Directeur general"&&x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep));
                //TOTEST
                if (listeDic.Contains(s.Poste.NomPoste))
                {
                    //Big boss
                    if (listeDic.Contains(s.Poste.NomPoste))
                    {
                        this.salaries.Find(x => x.Poste.NomPoste == "Directeur general").Employ.Add(s);
                    }
                   

                    //reste
                    if (Dep.Count == 0)
                        Console.WriteLine("none");
                    else
                    {
                        //listControleur
                        foreach (Salarie item in Dep)
                        {
                            if (item.Poste.getNumHierarchique() - s.Poste.getNumHierarchique()==1)//N-1
                                s.Employ.Add(item);
                        }

                        //orga
                        foreach (Salarie item in DepViaCeo)
                        {
                            if (item.Poste.getNumHierarchique() - s.Poste.getNumHierarchique()==1)
                                s.Employ.Add(item);
                        }

                    }
                    salaries.Add(s);
                }
                else
                {
                    Salarie chef = Dep.Find(x => x.Nom == nomSuperieur && x.Prenom == prenomSuperieur);
                    Salarie chefViaCeo = DepViaCeo.Find(x => x.Nom == nomSuperieur && x.Prenom == prenomSuperieur);
                    if (chef != null)
                    {
                        //list
                        chef.Employ.Add(s);
                        //orga
                        chefViaCeo.Employ.Add(s);
                        salaries.Add(s);
                    }
                    else
                    {
                        Console.WriteLine("\nNous n'avons pas le responsable de l'employé, operation impossible.");
                    }
                }
                //JSON PART
                try
                {
                    JsonUtil.sendJsonSalaries(this.salaries);
                    Console.WriteLine("Salarié ajouté a la base de donnée !");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error push into JSON file, message => " + e);
                }
            }
            else
            {
                Console.WriteLine("Le salarié " + s.Nom + " " + s.Prenom + " est déja dans nos base de données");
            }
            //refresh organnigramme
            BuildSalariesTree();
        }
        public void addSalarie(Salarie s)
        {
            //CEO PART;
            List<string> listeDic = new List<string> { "Directeur commercial", "Directeur des opérations", "Directeur financier", "Directeur RH" };
            //pour organigramme
            List<Salarie> DepViaCeo = this.salaries.Find((x) => x.Poste.NomPoste == "Directeur general").Employ.Find(x => x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep)).Employ;
            //TOTEST
           

            //NORMAL EMPLOYÉS PART
            if (!this.salaries.Contains(s))
            {
                if (listeDic.Contains(s.Poste.NomPoste))
                {
                    this.salaries.Find(x => x.Poste.NomPoste == "Directeur general").Employ.Add(s);
                }
                else
                {
                    /**to get only members of the same Departement**/
                    List<Salarie> Dep = salaries.FindAll((x) => x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep));
                    //Temporary
                    if (Dep.Count == 0)
                        Console.WriteLine("none");
                    else
                    {
                        foreach (Salarie item in Dep)
                        {
                            if (item.Poste.getNumHierarchique() < s.Poste.getNumHierarchique())
                                item.Employ.Add(s);
                            else if (item.Poste.getNumHierarchique() > s.Poste.getNumHierarchique())
                            {
                                s.Employ.Add(item);
                            }
                        }
                    }
                }
              
                salaries.Add(s);

                //JSON PART
                try
                {
                    JsonUtil.sendJsonSalaries(this.salaries);
                    Console.WriteLine("Salarié ajouté a la base de donnée !");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error push into JSON file, message => " + e);
                }
            }
            else
            {
                Console.WriteLine("Le salarié " + s.Nom + " " + s.Prenom + " est déja dans nos base de données");
            }
        }

        //TODO: modification Le nom, l’adresse, le mail, le téléphone, Le poste, le salaire
        public void updateSalarie(){}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String showOrgannigramme()
        {
            return this.organigramme.ToString();
        }
        /// <summary>
        /// Cette methode construie/update l'organigramme à partir de la liste de salariés 
        /// </summary>
        public void BuildSalariesTree()
        {
            Salarie CEO = this.Salaries.Find(x => x.Poste.NomPoste == "Directeur general");
            this.organigramme.Root.Key = CEO;
            //List without CEO
            List<Node<Salarie>> listeSalariesNode= new List<Node<Salarie>>();
            foreach (Salarie item in CEO.Employ)
            {
                listeSalariesNode.Add(SalarieTree.getaNewNode(item));
            }
            this.organigramme.Root.Childs = listeSalariesNode;
        }
        #endregion

        #region Clients
        /// <summary>
        /// this method show clients with or without "critère"
        /// async or not
        /// </summary>
        public void showClients(bool?simultane=null,List<ICritere>? critères=null)
        {
            if (critères != null)
            {
                int ind = 1;
                if (simultane!= null && !(bool)simultane)
                {
                    foreach (var item in critères)
                    {
                        Console.WriteLine("Votre critere numero " + ind+"\n");
                        this.clients.Sort((x, y) => item.operetaionTrie(x, y));
                        Console.WriteLine("Liste des clients: \n");
                        foreach (var item2 in this.clients)
                        {
                            Console.WriteLine(item2.ToString());
                        }
                        ind++;
                    }
                }
                else
                {
                    foreach (var item in critères)
                    {
                        Console.WriteLine("Chargement critere numero " + ind+"\n");
                        this.clients.Sort((x, y) => item.operetaionTrie(x, y));
                        ind++;
                    }
                    Console.WriteLine("Liste des clients final: \n");
                    foreach (var item2 in this.clients)
                    {
                        Console.WriteLine(item2.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Liste de clients:\n");
                foreach (var item in this.clients)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
        public void addClient(Client c) {
            if (!this.clients.Contains(c))
            {
                //APP PART
                this.clients.Add(c);

                //JSON PART
                try
                {
                    JsonUtil.sendJsonClients(this.clients);
                    Console.WriteLine("\nClient ajouté a la base de donnée !");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nError push into JSON file, message => " + e);
                }
            }
            else
            {
                Console.WriteLine("\nLe clients existe déjà !");
            }
               
        }
        public void deleteClient(string nom, string prenom) {
            var toDelete = this.Clients.Find(x => x.Nom.Equals(nom) && x.Prenom.Equals(prenom));
            if(toDelete != null)
            {
                //APP PART
                this.clients.Remove(toDelete);
                //JSON PART
                try
                {
                    JsonUtil.sendJsonClients(this.Clients);
                    Console.WriteLine("\nClient supprime de la base de donnee !");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nError push into JSON file, message => " + e);
                }
            }
            else
                Console.WriteLine("\nLe client " + nom + " " + prenom + " n'est pas dans notre base de donnee");


        }
        public void updateClient(string nom, string prenom)
        {
            Client toUpdate = this.clients.Find(x => x.Nom.Equals(nom) && x.Prenom.Equals(prenom));
            if (toUpdate != null)
            {
                Console.WriteLine("1.Modifier Nom et prénom\n2.Modifier adresse\n3.Exit");
                int choose = 1;
                while (choose > 0 && choose < 3)
                {
                    switch (choose)
                    {
                        case 1:
                            Console.WriteLine("\nVeuillez entrer le prenom du client: ");
                            String firstname = Console.ReadLine();
                            Console.WriteLine("\nVeuillez entrer le nom du client: ");
                            String lastname = Console.ReadLine();
                            toUpdate.Prenom = firstname;
                            toUpdate.Nom = lastname;
                            Console.WriteLine("\nNom et prénom modifiés !");
                            break;
                        case 2:
                            Console.WriteLine("\nVeuillez saisir le nom de la ville:");
                            string ville = Console.ReadLine();
                            Console.WriteLine("\nVeuillez saisir le nom de la rue");
                            string rue = Console.ReadLine();
                            Adresse newAd = new Adresse(ville,rue);
                            Console.WriteLine("\nAdresse modifié !");
                            break;
                    }
                    choose = 3;
                }
            }
            else
                Console.WriteLine("\nLe client que vous voulez modifier n'est pas dans notre BD");
        }
        #endregion

        #region Commandes
        //TOTEST:addCommande
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="from"></param>
        /// <param name="produit"></param>
        /// <param name="quantite"></param>
        /// <param name="chauffeur"></param>
        /// <param name="vehicule"></param>
        /// <param name="dateLiv"></param>
        public void addCommande(string nom,string prenom,string from, Produit produit, int quantite, Salarie chauffeur, Vehicule vehicule, DateTime? dateLiv = null) {
            Commande c = null;
            Client clt = this.clients.Find(x => x.Nom == nom && x.Prenom == prenom);
            if (clt == null)
            {
                Console.WriteLine("\nLe client n'est pas dans nos bases de donnees\n");
                clt = Client.createClient();
            }
            c = new Commande(clt, chauffeur, vehicule, produit, quantite, from, dateDeLivraison: dateLiv);
            this.commandes.Add(c);
            clt.doOrder(c);
            if (!this.clients.Contains(clt))
                this.addClient(clt);
            Console.WriteLine("Confirmation de: \n"+c.ToString());
            JsonUtil.sendJsonClients(this.Clients);
        }
        public void updateCommande(string nomClient, string nomChauffeur, DateTime date)
        {
            Commande c = this.commandes.Find(x => x.ClientCom.Nom == nomClient && x.ChauffeurCom.Nom == nomChauffeur && x.DateDeLivraison.ToString("d") == date.ToString("d"));
            if (c != null)
            {
                //TODO:Faire condition changement
            }
            else
                Console.WriteLine("\nil n'existe aucune livraison pour la date donné.");

        }
        public void showCommandes()
        {
            Console.WriteLine("Liste des commandes:\n");
            foreach (var item in this.commandes)
            {
                Console.WriteLine(item.ToString());
            }
        }
        #endregion

        #region Produit
        public void showProduitsAvailable()
        {
            Console.WriteLine("\n Liste de nos produits disponible:\n");
            foreach (var item in this.listeDesProduits)
            {
                Console.WriteLine(item.ToString());
            }
        }
        //TODO
        public void addNewProduict()
        {

        }
        //TODO
        public void deleteProduict(string nomProduit)
        {

        }
        #endregion
        #region Vehicule
        public void showVehiculeAvailable()
        {
            Console.WriteLine("\n Liste de nos vehicule disponible:\n");
            foreach (var item in this.ListeVehiculeDisponible)
            {
                Console.WriteLine(item.ToString());
            }
        }
        //TODO
        public void addNewVehicule()
        {

        }
        //TODO
        public void deleteVehicule(string nomProduit)
        {

        }
        #endregion


        #region Statistique
        //TOTEST:showChauffeurCommandesNumber
        /// <summary>
        /// 
        /// </summary>
        /// <param name="De"></param>
        /// <param name="A"></param>
        public void showChauffeurCommandesNumber(DateTime?De=null,DateTime?A=null)
        {
            List<Salarie> listeChauffeur = this.salaries.FindAll(x => x.Poste.NomPoste.Equals("Chauffeur"));
            if (De.HasValue && A.HasValue) {
                Console.WriteLine("\nDe "+((DateTime)De).ToString("d") + " À "+ ((DateTime)A).ToString("d")+": ");
                foreach (var item in listeChauffeur)
                {
                    List<Commande> commandeWithConstraint = ((Chauffeur)item.Poste).ListeDeCommandes.FindAll(x => x.DateDeLivraison >= De && x.DateDeLivraison <= A);
                    Console.WriteLine("\nNombre de commandes à/ou effectué par " + item.ToString() + ": " + commandeWithConstraint.Count());
                }
            }
            else
            {
                foreach (var item in listeChauffeur)
                {
                    Console.WriteLine("\nNombre de commandes à/ou effectué par "+item.ToString()+": "+((Chauffeur)item.Poste).ListeDeCommandes.Count());
                }
            }
        }
        //TOTEST:showAverageCommande
        /// <summary>
        /// 
        /// </summary>
        public void showAverageCommande()
        {
            double Average = this.commandes.Sum(x => x.Prix) / this.commandes.Count();
            Console.WriteLine("\n Le prix moyen des commandes est de: " + Average + " Euros");
        }
        //TOTEST:showAverageAchatCompteClient
        /// <summary>
        /// 
        /// </summary>
        public void showAverageAchatCompteClient()
        {
           foreach(var item in this.clients)
            {
                Console.WriteLine("\nMoyenne Achat pour "+item.ToString()+"Moyenne achat = "+item.getAverageCommande()+" Euros");
            }
            Console.WriteLine();
        }
        //TOTEST:showClientsListCommandes
        /// <summary>
        /// 
        /// </summary>
        public void showClientsListCommandes() {
            foreach (var item in this.clients)
            {
                Console.WriteLine("\nListe de commandes de "+item.ToString()+"Liste :\n"+item.getListCommande());
            }
            Console.WriteLine();
        }
        #endregion
    }
}
