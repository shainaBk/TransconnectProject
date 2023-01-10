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

		public static Vehicule createVehicule()
		{
			int num;

            do
			{
                Console.WriteLine("- Fonctionnalité creation Vehicule -\n");
                Console.WriteLine("Quel type de vehicule voulez vous ?\n1. Voiture\n2. Camionette\n3. Camion");
                Console.Write("\nVotre saisie: ");
                int.TryParse(Console.ReadLine(), out num);
            } while (num<0  || num>3 );
			Console.Clear();

			switch (num)
			{
				case 1:
					int place;
					string saisie;
                    do
					{
						Console.WriteLine("Veuillez choisir le nombre de place max de la voiture: ");
						Console.Write("\nVotre saisie: ");
						saisie = Console.ReadLine();
						Console.Clear();
					} while (!int.TryParse(saisie,out place));
					Console.WriteLine("Voiture cree !\n");
					return new Voiture(place);
					break;
				case 2:
                    string saisie2;
                    Console.WriteLine("Veuillez saisir l'usage de votre camionette: ");
                    Console.Write("\nVotre saisie: ");
                    saisie2 = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Camionette cree !\n");
                    return new Camionette(saisie2);
                    break;
				case 3:
                    string saisie3;
                    string saisie4;
                    Console.WriteLine("Veuillez saisir la matiere transporte par votre camion: ");
                    Console.Write("\nVotre saisie: ");
                    saisie3 = Console.ReadLine();
					string type;
					int kg;
                    do
                    {
                        Console.WriteLine("Veuillez les kg max transporte par votre camion: ");
                        Console.Write("\nVotre saisie: ");
                        saisie4 = Console.ReadLine();
                        Console.Clear();
                    } while (!int.TryParse(saisie4, out kg));
                    Console.Clear();
					if (new Random().Next(1, 3) < 2)
						type = "Camion-citerne";
					else
						type = "Camion benne";
                    Console.WriteLine("Camion cree !");
                    return new Camion(kg, saisie4,type);
                    break;
			}
			return null;
		}
    }
}

