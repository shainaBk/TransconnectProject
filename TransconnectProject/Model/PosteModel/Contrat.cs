﻿using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransconnectProject.Model.PosteModel
{
    public class Contrat : Poste
    {
        public Contrat() : base("Contrat", new DepRH(),1700)
        {
        }
    }
}
