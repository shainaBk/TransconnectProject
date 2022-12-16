using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class DirecteurFinancier : Poste
    {
        public DirecteurFinancier() : base("Directeur financier", new DepFinance(),9000)
        {
        }
    }
}
