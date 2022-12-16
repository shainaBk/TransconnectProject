using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class ChefEquipe : Poste
    {
        public ChefEquipe() : base("Chef d'équipe", new DepDesOps(),2500)
        {
        }
    }
}
