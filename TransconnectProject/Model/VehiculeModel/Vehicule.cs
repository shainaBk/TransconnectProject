using System;
namespace TransconnectProject.Model.VehiculeModel
{
	//TOTEST
	public abstract class Vehicule
	{
		public int prixLocation;//Pour un trajet quelconque
		private string nom;
		public Vehicule(int prixloca,string nom)
		{
			this.nom = nom;
			this.prixLocation = prixloca;
		}
		public int PrixLocation { get => this.prixLocation; set => this.prixLocation = value; }
		public string Nom { get => this.nom; set => this.nom = value; }

        public override string ToString()
        {
            return "Type de vehicule: "+this.nom+", prix de location: "+this.prixLocation;
        }
    }
}

