using System;
namespace TransconnectProject.Model.VehiculeModel
{
	public class Voiture:Vehicule
	{
		private int nombrePersonnes;
		public Voiture(int nbPersonnes):base(50)//Euro
		{
			this.nombrePersonnes = nbPersonnes;
		}
	}
}

