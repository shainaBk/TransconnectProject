using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransconnectProject.Model;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Util;
using TransconnectProject.Controleur.CritereClients;

namespace TransconnectProject.Controleur
{

    public class TransconnectControleur
    {
        private List<Salarie> salaries;
        private List<Client> clients;
        private SalarieTree organigramme;
        //TODO: Implanter methode Dijskra
        private PathCityWritter ptw;//dijskra tools

        public TransconnectControleur(List<Salarie> salaries,List<Client>?clients=null)
        {
            if (clients != null)
                this.clients = clients;
            else
                this.clients = new List<Client>();
            this.salaries = salaries;
            this.organigramme = new SalarieTree(new SalarieNode(null));
            ptw = new PathCityWritter();
            //BuildSalariesTree();//ATENTION POUR LE BIEN DE CERTAINS TESTS UNITS RETIRER
        }
        public List<Salarie> Salaries { get => this.salaries; set => this.salaries = value; }
        public List<Client> Clients { get => this.clients; set => this.clients = value; }
        public SalarieTree Organigramme { get => this.organigramme; set => this.organigramme = value; }
        public PathCityWritter Ptw { get => this.ptw;}

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
                        Console.WriteLine("Votre critère num " + ind);
                        this.clients.Sort((x, y) => item.operetaionTrie(x, y));
                        Console.WriteLine("Liste des clients: ");
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
                        Console.WriteLine("Chargement critère num " + ind);
                        this.clients.Sort((x, y) => item.operetaionTrie(x, y));
                        ind++;
                    }
                    Console.WriteLine("Liste des clients final: ");
                    foreach (var item2 in this.clients)
                    {
                        Console.WriteLine(item2.ToString());
                    }
                }
            }
            else
            {
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
                    Console.WriteLine("\nClient supprimé de la base de donnée !");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nError push into JSON file, message => " + e);
                }
            }
            else
                Console.WriteLine("\nLe client " + nom + " " + prenom + " n'est pas dans notre base de donnée");


        }
        public void updateClient(string nom, string prenom)
        {
            Client toUpdate = this.clients.Find(x => x.Nom.Equals(nom) && x.Prenom.Equals(prenom));
            if (toUpdate != null)
            {
                Console.WriteLine("1.Modifier Nom et prénom\n2.Modifier adresse\n3.Exit");
                int choose = int.Parse(Console.ReadLine());
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
                }
            }
            else
                Console.WriteLine("\nLe client que vous voulez modifier n'est pas dans notre BD");
        }
        #endregion

        #region Commandes
        //TODO
        public void addCommande() { }
        #endregion

        #region Statistique
        #endregion
    }
}
