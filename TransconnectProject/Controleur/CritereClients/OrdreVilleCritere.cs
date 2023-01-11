using System;
using TransconnectProject.Model;

namespace TransconnectProject.Controleur.CritereClients
{
	public class OrdreVilleCritere:ICritere
	{
        /// <summary>
        /// Sort by city order
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public int operetaionTrie(Client s1, Client s2)
        {
            return s1.Ville.CompareTo(s2.Ville);
        }
    }
}

