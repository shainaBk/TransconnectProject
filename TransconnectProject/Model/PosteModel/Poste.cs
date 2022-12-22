using System;
using TransconnectProject.Model.DepartementModel;
namespace TransconnectProject.Model.PosteModel
{
	public class Poste
	{
		private string nomPoste;
		private Departement departement;
        private double salaire;

        public Poste(string nom, Departement departement, double salaire)
        {
            this.nomPoste = nom;
            this.departement = departement;
            this.salaire = salaire;
        }
        
        public Departement Departement { get { return departement; } }
        public string NomPoste { get=>this.nomPoste; }    
        public double Salaire { get => this.salaire; }  
        public string getDepartementName() { return departement.NomDep; }

        public int getNumHierarchique()
        {
            return this.departement.getNumHierarchique(this.NomPoste);
        }

    }
}

