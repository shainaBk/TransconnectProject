using System;
using TransconnectProject.Model;

namespace TransconnectProject.Controleur.CritereClients
{
    public class OrdreMontantCumule : ICritere
    {
        /// <summary>
        /// Trie par achat cumulés
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public int operetaionTrie(Client s1, Client s2)
        {
            return s2.AchatCumulle.CompareTo(s1.AchatCumulle);
        }
    }
}

