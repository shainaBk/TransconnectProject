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

        public TransconnectControleur(List<Salarie> salaries,List<Client>?clients=null, List<Produit>? Produits = null, List<Vehicule>? vehicules= null,List<Commande>?commandes=null)
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
            if (commandes != null)
                this.commandes = commandes;
            else
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
        public List<Commande> ListeDesCommandes { get => this.commandes; set => this.commandes = value; }
        #region Salaries
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        public void deleteSalarie(string nom, string prenom)
        {
            var toDelete = this.salaries.Find(x => x.Nom.Equals(nom) && x.Prenom.Equals(prenom));
            Salarie vacant = new Salarie("Vacant", "Vacant", new DateTime(), null, null, null, new DateTime(), toDelete.Poste, toDelete.Employ);
            Salarie Ceo = this.salaries.Find(x => x.Poste.NomPoste == "Directeur general");
            if (toDelete != null)
            {
                List<string> listeDic = new List<string> { "Directeur commercial", "Directeur des operations", "Directeur financier", "Directeur RH" };

                if (listeDic.Contains(toDelete.Poste.NomPoste))
                {
                    Ceo.Employ.Remove(toDelete);
                    Ceo.Employ.Add(vacant);
                    this.salaries.Remove(toDelete);
                    this.salaries.Add(vacant);
                }
                else
                {
                    if (toDelete.Employ.Count() > 0)
                    {
                        List<Salarie> degOne = Ceo.Employ;
                        foreach (var item in degOne)
                        {
                            if (item.Employ.Contains(toDelete))
                            {
                                item.Employ.Remove(toDelete);
                                item.Employ.Add(vacant);
                            }
                        }
                        this.salaries.Remove(toDelete);
                        this.salaries.Add(vacant);
                    }
                    else
                    {
                        List<Salarie> degOne = Ceo.Employ;
                        foreach (var item in degOne)
                        {
                            if (item.Employ.Contains(toDelete))
                                item.Employ.Remove(toDelete);
                            else
                            {
                                foreach (var item2 in item.Employ)
                                {
                                    if (item2.Employ.Contains(toDelete))
                                        item2.Employ.Remove(toDelete);
                                }
                            }
                        }
                        this.salaries.Remove(toDelete);
                    }
                }
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
        public void addSalarie(Salarie s,string nomSuperieur =null, string prenomSuperieur=null)
        {
            if (!this.salaries.Contains(s))
            {
                bool ok = false;
                List<string> listeDic = new List<string> { "Directeur commercial", "Directeur des operations", "Directeur financier", "Directeur RH" };
                //pour organigramme
                List<Salarie> DepViaCeo = this.salaries.Find((x) => x.Poste.NomPoste == "Directeur general").Employ.Find(x=>x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep)).Employ;
                //Pour liste controleur
                List<Salarie> Dep = this.salaries.FindAll((x) => x.Poste.NomPoste!= "Directeur general"&&x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep));
                //TOTEST
                if (listeDic.Contains(s.Poste.NomPoste))
                {
                    Salarie old = this.salaries.Find(x => x.Poste.NomPoste == s.Poste.NomPoste);
                    Salarie oldViaBoss = this.salaries.Find(x => x.Poste.NomPoste == "Directeur general").Employ.Find(x => x.Poste.NomPoste == s.Poste.NomPoste);
                    s.Employ = oldViaBoss.Employ;
                    this.salaries.Find(x => x.Poste.NomPoste == "Directeur general").Employ.Remove(oldViaBoss);
                    this.salaries.Find(x => x.Poste.NomPoste == "Directeur general").Employ.Add(s);
                    this.salaries.Remove(old);
                    this.salaries.Add(s);
                    ok = true;
                }
                else
                {
                    if (nomSuperieur != null)
                    {
                        Salarie chef = Dep.Find(x => x.Nom == nomSuperieur && x.Prenom == prenomSuperieur);
                        Salarie oldViaChef = chef.Employ.Find(x => x.Poste.NomPoste == s.Poste.NomPoste && x.Nom == "Vacant");
                        Salarie chefViaCeo = DepViaCeo.Find(x => x.Nom == nomSuperieur && x.Prenom == prenomSuperieur && x.Nom == "Vacant");
                        Salarie oldViachefViaCeo = chefViaCeo.Employ.Find(x => x.Poste.NomPoste == s.Poste.NomPoste);
                        if (chef != null)
                        {
                            if(oldViachefViaCeo != null)
                            {
                                s.Employ = oldViachefViaCeo.Employ;
                                chefViaCeo.Employ.Remove(oldViaChef);
                                oldViachefViaCeo.Employ.Remove(oldViachefViaCeo);
                            }
                            //list
                            chef.Employ.Add(s);
                            //orga
                            chefViaCeo.Employ.Add(s);
                            salaries.Add(s);
                            ok = true;
                        }
                    }
                    else
                    {
                        /**to get only members of the same Departement**/
                        List<Salarie> Dep2 = salaries.FindAll((x) => x.Poste.NomPoste != "Directeur general" && x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep));
                        Salarie old = Dep2.Find(x => x.Poste.NomPoste == s.Poste.NomPoste && x.Nom == "Vacant");
                        Salarie old2 = this.salaries.Find((x) => x.Poste.NomPoste == "Directeur general").Employ.Find(x => x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep)).Employ.Find(x => x.Poste.NomPoste == s.Poste.NomPoste && x.Nom == "Vacant");
                        //Temporary
                        if (Dep2.Count == 0)
                            Console.WriteLine("none");
                        else
                        {
                            if (old2 != null)
                            {
                                s.Employ = old2.Employ;
                                this.salaries.Find((x) => x.Poste.NomPoste == "Directeur general").Employ.Find(x => x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep)).Employ.Remove(old2);
                                this.salaries.Remove(old);
                            }
                            this.salaries.Find((x) => x.Poste.NomPoste == "Directeur general").Employ.Find(x => x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep)).Employ.Add(s);
                            this.salaries.Add(s);
                            ok = true;
                        }
                    }
                }
                //JSON PART
                try
                {
                    if (ok)
                    {
                        JsonUtil.sendJsonSalaries(this.salaries);
                        Console.WriteLine("Salarie ajoute a la base de donnee !");
                    }
                    else throw new Exception("Impossible de push !");
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error push into JSON file, message => " + e);
                }
            }
            else
            {
                Console.WriteLine("Le salarie " + s.Nom + " " + s.Prenom + " est deja dans nos base de donnees");
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
        public void updateClient(Client toUpdate)
        {
            Console.Clear();
            int choose;
            do
            {
                Console.WriteLine("\nVeuillez saisir ce que vous voulez modifier: \n");
                Console.WriteLine("1. Modifier Nom et prénom\n2. Modifier adresse\n3. Exit");
                Console.Write("\nvotre saisie: ");
                string input = Console.ReadLine();
                
                while (!int.TryParse(input, out choose))
                {
                    Console.WriteLine("\nSorry, nous n'avons pas compris votre saisie...\n");
                    Console.WriteLine("\nVeuillez saisir ce que vous voulez modifier\n: ");
                    Console.WriteLine("1. Modifier Nom et prenom\n2. Modifier adresse\n3. Exit");
                    Console.Write("\nvotre saisie: ");
                    input = Console.ReadLine();
                    Console.Clear();
                }
                if(choose < 0 || choose > 3)
                    Console.WriteLine("\nErreur, la saisie est hors borne...");
            } while (choose < 0 || choose > 3);

            switch (choose)
             {
                case 1:
                    Console.Clear();
                    Console.WriteLine("\nVeuillez entrer le prenom du client: ");
                    String firstname = Console.ReadLine();
                    Console.WriteLine("\nVeuillez entrer le nom du client: ");
                    String lastname = Console.ReadLine();
                    toUpdate.Prenom = firstname;
                    toUpdate.Nom = lastname;
                    JsonUtil.sendJsonClients(clients);
                    Console.WriteLine("\nNom et prénom modifiés !\n");
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("\nVeuillez saisir le nom de la ville:");
                    string ville = Console.ReadLine();
                    Console.WriteLine("\nVeuillez saisir le nom de la rue");
                    string rue = Console.ReadLine();
                    Adresse newAd = new Adresse(ville,rue);
                    toUpdate.Adresse = newAd;
                    JsonUtil.sendJsonClients(clients);
                    Console.WriteLine("\nAdresse modifié !\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nExit ! Bye");
                    break;
            }
            JsonUtil.sendJsonClients(this.clients);
        }
        #endregion
        #region Commandes
        public void updateCommande(Commande toUpdate)
        {
            Console.Clear();
            int choose;
            do
            {
                Console.WriteLine("\nVeuillez saisir ce que vous voulez modifier: \n");
                Console.WriteLine("1. Ville de livraison\n2. Date de livraison\n3. Exit");
                Console.Write("\nvotre saisie: ");
                string input = Console.ReadLine();

                while (!int.TryParse(input, out choose))
                {
                    Console.WriteLine("\nSorry, nous n'avons pas compris votre saisie...\n");
                    Console.WriteLine("\nVeuillez saisir ce que vous voulez modifier\n: ");
                    Console.WriteLine("1. Ville de livraison\n2. Date de livraison\n3. Exit");
                    Console.Write("\nvotre saisie: ");
                    input = Console.ReadLine();
                    Console.Clear();
                }
                if (choose < 0 || choose > 3)
                    Console.WriteLine("\nErreur, la saisie est hors borne...");
            } while (choose < 0 || choose > 3);

            switch (choose)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("\nVeuillez entrer le nom de la ville: ");
                    string v = Console.ReadLine();
                    foreach (var item in clients)
                    {
                        Commande find = item.CommandesClient.Find(x=>x.ProprietaireNom == toUpdate.ProprietaireNom);
                        if (find != null)
                        {
                            find.VilleB = v;
                            find.updatePath();
                        }
                            
                    }
                    toUpdate.VilleB = v;
                    toUpdate.updatePath();
                    JsonUtil.sendJsonClients(clients);
                    Console.WriteLine("\nLa ville de livraision a bien ete modifie !\n");
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("\nVeuillez saisir la date de livraison (yyyy/mm/dd):");
                    string date = Console.ReadLine();
                    foreach (var item in clients)
                    {
                        Commande find = item.CommandesClient.Find(x => x.ProprietaireNom == toUpdate.ProprietaireNom);
                        if (find != null)
                            find.DateDeLivraison = new DateTime(int.Parse(date.Split("/")[0]), int.Parse(date.Split("/")[1]), int.Parse(date.Split("/")[2])); ;
                    }
                    toUpdate.DateDeLivraison = new DateTime(int.Parse(date.Split("/")[0]), int.Parse(date.Split("/")[1]),int.Parse(date.Split("/")[2]));
                    JsonUtil.sendJsonClients(clients);
                    Console.WriteLine("\nDate modifié !\n");
                    break;
                default:
                    Console.WriteLine("\nExit ! Bye");
                    break;
            }
        }
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
            c = new Commande(clt.Nom,clt.Prenom, chauffeur, vehicule, produit, quantite, from,clt.Ville, dateDeLivraison: dateLiv);
            ((Chauffeur)c.ChauffeurCom.Poste).addCommande(c);
            this.commandes.Add(c);
            clt.doOrder(c);
            if (!this.clients.Contains(clt))
                this.addClient(clt);
            Console.WriteLine("Confirmation de: \n"+c.ToString());
            JsonUtil.sendJsonClients(this.Clients);
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
        public void showAveragePriceCommandes()
        {
            double price = this.commandes.Count() > 0 ? this.commandes.Average(x => x.getPrice()) : 0.0;
            Console.WriteLine("\nPrix moyen des commandes = " + price + "\n");
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
                Console.WriteLine("\nListe de commandes de "+item.ToString()+"\nListe : "+item.getListCommande());
            }
            Console.WriteLine();
        }
        #endregion
    }
}
