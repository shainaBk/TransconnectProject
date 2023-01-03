using System;
namespace TransconnectProject.Model.DepartementModel
{
	public class DepDesOps:Departement
	{
		public DepDesOps() : base("Département des operations", new Dictionary<string, int>
        {
            {"Directeur des operations", 1 },
            {"Chef d'équipe", 2},
            {"Chauffeur", 3}
            })
        { }
    }
}

