using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransconnectProject.Model;

namespace TransconnectProject.Controleur
{

    public class TransconnectControleur
    {
        private List<Salarie> salaries;
        private List<Client> clients;

        public TransconnectControleur(List<Salarie> salaries)
        {
            this.salaries = salaries;
        }

        public void addSalarie(Salarie s)
        {
            /**Get only members of the same Departement**/
            List<Salarie> Dep = salaries.FindAll((x) => x.Poste.Departement.NomDep.Equals(s.Poste.Departement.NomDep));
            /** ADD Condition si existe pas; Peut etre exception ?**/
            //Temporary
            if (Dep.Count == 0)
                Console.WriteLine("none");
            else
            {
                foreach (Salarie item in Dep)
                {
                    if (item.Poste.getNumHierarchique() < s.Poste.getNumHierarchique())
                        item.Employ.Add(s);
                }
            }
            salaries.Add(s);
            //Log("Employé ajouté !!)
        }
    }
}
