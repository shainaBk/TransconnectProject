using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    internal class Comptable : Poste
    {
        public Comptable() : base("Comptable", new DepFinance(),1700)
        {
        }
    }
}
