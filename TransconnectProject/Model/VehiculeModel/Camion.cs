using System;
namespace TransconnectProject.Model.VehiculeModel
{
	//TOTEST
	//TODO: link with des produit à dispositions
	public class Camion:Vehicule
	{
		private int volumeTransportMax;//VolumeMax
		private string matiereTransport;//matières transportés
		private string nomCamion;
        public Camion(int volTransport,string matiere,string nomCam):base(250)
		{
			this.matiereTransport = matiere;
			this.volumeTransportMax = volTransport;
			this.nomCamion = nomCam;
		}
	}
}

