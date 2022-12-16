using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class DirecteurDesOps : Poste
    {
        public DirecteurDesOps() : base("Directeur des opérations", new DepDesOps(), 7000)
        {
        }
    }
}
