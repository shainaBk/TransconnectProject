using System;
using TransconnectProject.Model.DepartementModel;
namespace TransconnectProject.Model.PosteModel
{
	public abstract class Poste
	{
		private string nom;
		private Departement departement;
        private double salaire;

        public Poste(string nom, Departement departement, double salaire)
        {
            this.nom = nom;
            this.departement = departement;
            this.salaire = salaire;
        }
        
        public Departement Departement { get { return departement; } }
        public string Nom { get=>this.nom; }    
        public double Salaire { get => this.salaire; }  
        public string getDepartementName() { return departement.Nom; }

        public int getNumHierarchique()
        {
            return this.departement.getNumHierarchique(this.Nom);
        }

    }
}

