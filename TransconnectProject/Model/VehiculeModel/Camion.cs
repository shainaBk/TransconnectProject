using System;
namespace TransconnectProject.Model.VehiculeModel
{
	//TOTEST
	//TODO: GERER DEPENDANCE TRANSPORT PRODUIT ET QUANTITÉ MAX
	public class Camion:Vehicule
	{
		private int volumeTransportMax;//VolumeMax
		private string matiereTransport;//matières transportés
        public Camion(int volTransport,string matiere,string nomCam):base(250,nomCam)
		{
			this.matiereTransport = matiere;
			this.volumeTransportMax = volTransport;
		}
		public int VolumeTransportMax { get => this.volumeTransportMax; set => this.volumeTransportMax=value; }
		public string MatiereTransport { get => this.matiereTransport; set => this.matiereTransport = value; }
        public override string ToString()
        {
            return base.ToString()+", matiere transport: "+matiereTransport+", volume de transport Max: "+volumeTransportMax+" Kg\n";
        }
    }
}

