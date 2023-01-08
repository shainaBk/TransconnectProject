using System;
namespace TransconnectProject.Model.VehiculeModel
{
	public class Voiture:Vehicule
	{
		private int nombrePersonnes;
		public Voiture(int nbPersonnes):base(50,"Voiture")//Euro
		{
			this.nombrePersonnes = nbPersonnes;
		}
        public override string ToString()
        {
            return base.ToString()+", nombre places disponoble: "+this.nombrePersonnes+"\n";
        }
    }
}

