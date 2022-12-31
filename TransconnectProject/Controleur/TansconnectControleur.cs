using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransconnectProject.Model;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Util;

namespace TransconnectProject.Controleur
{

    public class TransconnectControleur
    {
        private List<Salarie> salaries;
        private List<Client> clients;
        private SalarieTree organigramme;
        //TODO: Implanter methode Dijskra
        private PathCityWritter ptw;//dijskra tools

        public TransconnectControleur(List<Salarie> salaries)
        {
            this.salaries = salaries;
            this.organigramme = new SalarieTree(new SalarieNode(null));
            ptw = new PathCityWritter();
        }
        public List<Salarie> Salaries { get => this.salaries; set => this.salaries = value; }
        public SalarieTree Organigramme { get => this.organigramme; set => this.organigramme = value; }
        public PathCityWritter Ptw { get => this.ptw;}

        #region Salaries
        public void deleteSalarie(string nom, string prenom)
        {
            var toDelete = this.salaries.Find(x => x.Nom.Equals(nom) && x.Prenom.Equals(prenom));
            //CEO PART;
            List<string> listeDic = new List<string> { "Directeur commercial", "Directeur des opérations", "Directeur financier", "Directeur RH" };
            //TOTEST
            if (listeDic.Contains(toDelete.Poste.NomPoste))
            {
                this.salaries.Find(x => x.Poste.NomPoste == "Directeur general").Employ.Remove(toDelete);
            }

            //NORMAL EMPLOYÉS PART
            List<Salarie> lesColegues = this.salaries.FindAll(x => x.Poste.Departement.NomDep.Equals(toDelete.Poste.Departement.NomDep) && x.Poste.getNumHierarchique() < toDelete.Poste.getNumHierarchique());
            if (lesColegues != null)
            {
                foreach (var item in lesColegues)
                {
                    item.Employ.Remove(toDelete);
                }
            }
            this.salaries.Remove(toDelete);

            //JSON PART
            if(toDelete != null)
            {
                try
                {
                    JsonUtil.sendJsonSalaries(this.salaries);
                    Console.WriteLine("Salarié supprimé de la base de donnée !");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error push into JSON file, message => " + e);
                }
            }
            else
                Console.WriteLine("Le salarie "+nom+" "+prenom+" n'est pas dans notre base de donnée");
        }
        public void addSalarie(Salarie s)
        {
            //CEO PART;
            List<string> listeDic = new List<string> { "Directeur commercial", "Directeur des opérations", "Directeur financier", "Directeur RH" };
            //TOTEST
            if (listeDic.Contains(s.Poste.NomPoste))
            {
                this.salaries.Find(x => x.Poste.NomPoste == "Directeur general").Employ.Add(s);
            }

            //NORMAL EMPLOYÉS PART
            if (!this.salaries.Contains(s))
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
                Console.WriteLine("Le salarié "+s.Nom+" "+s.Prenom+" est déja dans nos base de données");
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
        //TODO
        /// <summary>
        /// this method show clients with or without "critère"
        /// </summary>
        public void showClients()
        {
        }
        //TODO
        public void addClient() { }
        //TODO
        public void deleteClient() { }
        #endregion

        #region Commandes
        //TODO
        public void addCommande() { }
        #endregion

        #region Statistique
        #endregion
    }
}
