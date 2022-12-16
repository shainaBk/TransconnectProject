using System;
namespace TransconnectProject.Model.DepartementModel
{
	public class DepCommercial:Departement
	{
		public DepCommercial() : base("Département commercial", new Dictionary<string, int>
        {
            {"Directeur commercial", 1 },
            {"Commercial ", 2}
            })
        { }
    }
}

