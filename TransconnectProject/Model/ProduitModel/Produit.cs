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
		public double PrixKg { get => this.prixKg; set => this.prixKg = value;}
		public string NomProduit { get => this.nomProduit; set => this.nomProduit = value;}
        public override string ToString()
        {
            return "Nom du produit: "+this.nomProduit+", Prix au Kg: "+this.prixKg+" Euros\n";	
        }

		public static Produit createProduit()
		{
			string priceKg ="";
            double kg;
            Console.WriteLine("\n- Fonctionnalite de creation de Produit -\n");
            Console.Write("veuillez saisir le nom du Produit: ");
            string nom = Console.ReadLine();
			do
			{
                Console.WriteLine();
                Console.Write("Veuillez saisir le Prix au kilo: ");
                priceKg = Console.ReadLine();
				Console.Clear();
            } while (!double.TryParse(priceKg,out kg));
            
            Produit p = new Produit(nom,kg);
			if(p!=null)
				Console.WriteLine("Produit cree !\n");
			return p;
		}
    }
}

