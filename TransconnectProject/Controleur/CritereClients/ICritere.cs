using System;
using TransconnectProject.Model;
namespace TransconnectProject.Controleur.CritereClients
{
	public interface ICritere
	{
		/// <summary>
		/// Comparaison pour un critère specifique
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns></returns>
		int operetaionTrie(Client s1,Client s2);
	}
}

