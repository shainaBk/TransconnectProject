using System;
using TransconnectProject.Model;

namespace TransconnectProject.Controleur.CritereClients
{
	public class OrdreAlphaCritere:ICritere
	{
        /// <summary>
        /// Trie dans l'ordre alpha
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public int operetaionTrie(Client s1, Client s2)
        {
            if (s1.Prenom.CompareTo(s2.Prenom) > 0 && s1.Nom.CompareTo(s2.Nom) > 0 || s1.Prenom.CompareTo(s2.Prenom) < 0 && s1.Nom.CompareTo(s2.Nom) > 0)
                return 1;
            else if (s1.Prenom.CompareTo(s2.Prenom) == 0 && s1.Nom.CompareTo(s2.Nom) == 0)
                return 0;
            else
                return -1;
        }
    }
}

