using System;
namespace TransconnectProject.Model.VehiculeModel
{
	public class Camionette:Vehicule
	{
		private string usage;
		public Camionette(string usage):base(150,"Camionette")//Euro
		{
			this.usage = usage;
		}
        public override string ToString()
        {
			return base.ToString() + ", usage: " + this.usage+"\n";
        }
    }
}

