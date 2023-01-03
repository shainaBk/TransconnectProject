using System;
using TransconnectProject.Model;

namespace TransconnectProject.Controleur.CritereClients
{
    public class OrdreMontantCumule : ICritere
    {
        public int operetaionTrie(Client s1, Client s2)
        {
            return s1.AchatCumulle.CompareTo(s2.AchatCumulle);
        }
    }
}

