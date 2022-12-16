using System;
namespace TransconnectProject.Model.DepartementModel
{
	public class DepDesOps:Departement
	{
		public DepDesOps() : base("Département des opérations", new Dictionary<string, int>
        {
            {"Directeur des opérations", 1 },
            {"Chef d'équipe", 2},
            {"Chauffeur", 3}
            })
        { }
    }
}

