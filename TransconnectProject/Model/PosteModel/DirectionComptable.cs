using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class DirectionComptable : Poste
    {
        public DirectionComptable() : base("Direction comptable", new DepFinance(),3000)
        {
        }
    }
}
