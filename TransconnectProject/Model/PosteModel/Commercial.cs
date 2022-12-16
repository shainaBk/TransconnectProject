using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class Commercial : Poste
    {
        public Commercial() : base("Commercial", new DepCommercial(),2500)
        {
        }
    }
}
