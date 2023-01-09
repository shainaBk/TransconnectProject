using System;
using Newtonsoft.Json.Linq;
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

        public static Poste getPoste(string name)
        {

            if (name == "Chauffeur")
                return new Chauffeur();
            else if (name == "Chef d'équipe")
                return new ChefEquipe();
            else if (name == "Directeur des operations")
                return new DirecteurDesOps();
            else if (name == "Comptable")
                return new Comptable();
            else if (name == "Commercial")
                return new Commercial();
            else if (name == "Contrat")
                return new Contrat();
            else if (name == "Controleur de gestion")
                return new ControleurDeGestion();
            else if (name == "Directeur commercial")
                return new DirecteurCommercial();
            else if (name == "Directeur financier")
                return new DirecteurFinancier();
            else if (name == "Directeur general")
                return new DirecteurGeneral();
            else if (name == "Directeur RH")
                return new DirecteurRH();
            else if (name == "Direction comptable")
                return new DirectionComptable();
            else if (name == "Formation")
                return new Formation();
            else
                return null;
        }

    }
}

