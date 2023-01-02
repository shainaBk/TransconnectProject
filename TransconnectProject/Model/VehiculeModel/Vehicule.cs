using System;
namespace TransconnectProject.Model.VehiculeModel
{
	//TOTEST
	public abstract class Vehicule
	{
		private int prixLocation;//Pour un trajet quelconque 
		public Vehicule(int prixloca)
		{
			this.prixLocation = prixloca;
		}
		public int PrixLocation { get => this.prixLocation; }
	}
}

