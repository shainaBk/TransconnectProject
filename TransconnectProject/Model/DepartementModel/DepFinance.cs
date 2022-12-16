using System;
using TransconnectProject.Model.PosteModel;
namespace TransconnectProject.Model.DepartementModel
{
	public class DepFinance:Departement
	{
		public DepFinance():base("Département des Finances",new Dictionary<string, int>
        {
            {"Directeur financier", 1 },
            {"Direction comptable", 2},
			{"Controleur de gestion", 2},
            {"Comptable", 3}
			})
		{ }
		
	}
}

