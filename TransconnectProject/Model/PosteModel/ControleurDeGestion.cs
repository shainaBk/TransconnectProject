using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class ControleurDeGestion : Poste
    {
        public ControleurDeGestion() : base("Controleur de gestion", new DepFinance(),2300)
        {
        }
    }
}
