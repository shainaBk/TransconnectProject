using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class Chauffeur : Poste
    {
        public Chauffeur() : base("Chauffeur", new DepDesOps(),1600)
        {
        }

        //TODO
        public int getTarif() { return 0; }
    }
}
