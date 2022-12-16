using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    internal class Formation : Poste
    {
        public Formation() : base("Formation", new DepRH(),1700)
        {
        }
    }
}
