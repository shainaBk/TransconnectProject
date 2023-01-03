using System;
using TransconnectProject.Model;

namespace TransconnectProject.Controleur.CritereClients
{
	public class OrdreVilleCritere:ICritere
	{
        public int operetaionTrie(Client s1, Client s2)
        {
            return s1.Ville.CompareTo(s2.Ville);
        }
    }
}

