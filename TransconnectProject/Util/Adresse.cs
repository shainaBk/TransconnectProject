using System;
namespace TransconnectProject.Util
{
	public class Adresse
	{
		string ville;
		string rue;
		public Adresse(string ville, string rue)
		{
			this.ville = ville;
			this.rue = rue;
		}
		public string Ville{
			get => this.ville;
			set
			{
				this.ville = value;
			}
		}
	}
}

