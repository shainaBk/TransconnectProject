using System;
using TransconnectProject.Model.PosteModel;
namespace TransconnectProject.Model.DepartementModel
{
	public abstract class Departement
	{
		string nom;
		Dictionary<String, int> hierarchie;

        public Departement(string nom, Dictionary<String, int> hierarchie)
        {
            this.nom = nom;
            this.hierarchie = hierarchie;
        }
        public string Nom { get => this.nom; set { this.nom = value; } }
        public Dictionary<string, int> Hierarchie { get => this.hierarchie; set { this.hierarchie = value; } }

        public int getNumHierarchique(String poste)//Usefull for add salarié
        {
            foreach (var item in Hierarchie)
            {
                if(item.Key.Equals(poste))
                    return item.Value;
            }
            return -1;
        }

    }
}

