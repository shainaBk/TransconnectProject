using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class DirecteurRH : Poste
    {
        public DirecteurRH() : base("Directeur RH", new DepRH(),8000)
        {
        }
    }
}
