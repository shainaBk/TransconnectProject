using System;
namespace TransconnectProject.Model.VehiculeModel
{
	public class Camionette:Vehicule
	{
		private string usage;
		public Camionette(string usage):base(150)//Euro
		{
			this.usage = usage;
		}
	}
}

