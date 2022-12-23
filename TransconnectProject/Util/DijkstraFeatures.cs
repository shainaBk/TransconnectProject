using System;
namespace TransconnectProject.Util
{
	/*STEP
	 * Int.Maxvalue == infini
	 * 1) create liste des villes complete
	 * 2) create Matrice ville ville poids
	 * 3)
	 * */
	public class DijkstraFeatures
	{
		public DijkstraFeatures()
		{
		}

        /// <summary>
        /// This methode return the city with the lightweight node
        /// </summary>
        /// <param name="Distance"></param>
        /// <param name="ShortestPathProcessed"></param>
        /// <returns></returns>
        public static string getMinDistanceInd(Dictionary<string, int> Distance, Dictionary<string, bool> ShortestPathProcessed)
        {
			int min = int.MaxValue;
			string index = null;
            foreach (KeyValuePair<string, int> item in Distance)
            {
				if (ShortestPathProcessed[item.Key]==false && item.Value <= min)
				{
					min = item.Value;
					index = item.Key;
				}
            }
            return index;
        }
		//TOTEST: ABSOLUMENT
		/// <summary>
		/// Dijkra algo
		/// </summary>
		/// <param name="arcs"></param>
		/// <param name="villeDepart"></param>
		/// <param name="listeVille"></param>
		/// <param name="nombreVille"></param>
		public static void Dijkstra(int[,]arcs ,string villeDepart, string[]listeVille, int nombreVille)
		{
			//Distance depuis villeDepart
			Dictionary<string, string> cityParent = new Dictionary<string, string>(nombreVille); ;
			Dictionary<string, int> Distance = new Dictionary<string, int>(nombreVille);
            Dictionary<string, bool> ShortestPathProcessed = new Dictionary<string, bool>(nombreVille);

			foreach (string item in listeVille)
			{
				Distance.Add(item, int.MaxValue);
				ShortestPathProcessed.Add(item, false);
				cityParent.Add(item, null);
			}

			Distance[villeDepart] = 0;

			for(int u=0;u<nombreVille;u++)//peut-être nombreville-1
			{
				string lightweightCity = getMinDistanceInd(Distance, ShortestPathProcessed);
				ShortestPathProcessed[lightweightCity] = true;

                for(int v=0;v<nombreVille;v++)
                {
					if (!ShortestPathProcessed.ElementAt(v).Value && Convert.ToBoolean(arcs[u,v]) && Distance.ElementAt(u).Value != int.MaxValue && Distance.ElementAt(u).Value+ arcs[u, v] < Distance.ElementAt(u).Value)//Convert.ToBoolean(arcs[u,v]) false si 0
                    {
						Distance[Distance.ElementAt(u).Key] = Distance.ElementAt(u).Value + arcs[u, v];
						cityParent[Distance.ElementAt(u).Key] = Distance.ElementAt(v).Key;
                    }

                }
            }
        }
	}

	public class PathCityWriter
	{
		private string cityA;
        private string cityB;
        private int distance;

		public string CityA { get => this.cityA; set => cityA = value; }
		public string CityB { get => this.cityB; set => cityB = value; }
		public int Distance { get => this.distance; set => distance = value; }


	}
}

