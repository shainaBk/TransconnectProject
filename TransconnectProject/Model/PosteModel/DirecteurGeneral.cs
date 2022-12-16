using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class DirecteurGeneral : Poste
    {
        public DirecteurGeneral() : base("Directeur general", null,15000)
        {
        }
    }
}
