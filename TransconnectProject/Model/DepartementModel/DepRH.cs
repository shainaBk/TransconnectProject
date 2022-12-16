using System;
namespace TransconnectProject.Model.DepartementModel
{
	public class DepRH:Departement
	{
		public DepRH() :base("Département Ressources Humaines", new Dictionary<string, int>
        {
            {"Directeur RH", 1 },
            {"Formation", 2},
            {"Contract", 2}
            })
        { }
    }
}

