using System;
namespace TransconnectProject.Model.ProduitModel
{
	//TOTEST
	public class Produit
	{
		private string nomProduit;
		private double prixKg;//prix au kg
		public Produit(string nomProd,double pricekg)
		{
			this.nomProduit = nomProd;
			this.prixKg = pricekg;
		}
		public double PrixKg { get => this.prixKg; }
		public string NomProduit { get => this.nomProduit; }
	}
}

