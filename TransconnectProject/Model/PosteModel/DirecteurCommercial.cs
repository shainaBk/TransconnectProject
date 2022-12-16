using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class DirecteurCommercial : Poste
    {
        public DirecteurCommercial() : base("Directeur commercial", new DepCommercial(),9000)
        {
        }
    }
}
