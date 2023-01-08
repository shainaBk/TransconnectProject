using System;
using System.Drawing;
using System.Globalization;
using CsvHelper;

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


		/// <summary>
		/// Dijkra algo
        /// (showmode quand il faut afficher le chemin)
		/// </summary>
		/// <param name="arcs"></param>
		/// <param name="villeDepart"></param>
		/// <param name="listeVille"></param>
		/// <param name="nombreVille"></param>
		public static int Dijkstra(int[,]arcs ,string villeDepart,string villeArrive, HashSet<string> listeVille,PathCityWritter? ptw = null)
		{
            //Distance depuis villeDepart
            bool noWay = false;
			int nombreVille = listeVille.Count();
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

			for(int count=0;count<nombreVille-1;count++)//peut-être nombreville-1
			{
				string lightweightCity = getMinDistanceInd(Distance, ShortestPathProcessed);//c'est sensé etre u ! //TODO: getMinDistanceInd return u
                ShortestPathProcessed[lightweightCity] = true;

                int u = Array.IndexOf(Distance.Keys.ToArray(), lightweightCity);

                for (int v = 0; v < nombreVille; v++) { 

                    if (!ShortestPathProcessed.ElementAt(v).Value && Convert.ToBoolean(arcs[u,v]) && (Distance.ElementAt(u).Value != int.MaxValue)&&(arcs[u, v]!=int.MaxValue) && (Distance.ElementAt(u).Value+ arcs[u, v] < Distance.ElementAt(v).Value))//Convert.ToBoolean(arcs[u,v]) false si 0
                    {
						Distance[Distance.ElementAt(v).Key] = Distance.ElementAt(u).Value + arcs[u, v];
						cityParent[Distance.ElementAt(v).Key] = Distance.ElementAt(u).Key;
                    }

                }
            }
            string outString = "";
            noWay = Distance[villeArrive] == int.MaxValue?true:false;//Si aucun chemin n'existe
			if (!noWay)
			{
                /*Console.WriteLine("\n"+Distance[villeArrive] + "Km entre " + villeDepart + " et " + villeArrive + " !!");
                Console.WriteLine();*/
                string currentDad = villeArrive;
                //Console.Write("Chemin: " + villeArrive + " <-- ");
                //output part
                outString = "\n" + Distance[villeArrive] + "Km entre " + villeDepart + " et " + villeArrive + " !!\n Chemin: " + villeArrive + " <-- ";
                do
                {
                    currentDad = cityParent[currentDad];
                    if (currentDad == null) break;
                    if (currentDad == villeDepart)
                    {
                        //Console.Write(currentDad + "\n");
                        //output part
                        outString += currentDad;
                    }
                    else
                    {
                        //Console.Write(currentDad + " <-- ");
                        //output part
                        outString += currentDad + " <-- ";
                    }
                       
                } while (currentDad != null);
            }
            else if (noWay)
            {
                Console.WriteLine("Aucun chemin !");
                outString = "Aucun chemin !";
                return -1;
            }

            if (ptw != null)
                ptw.CurrentPath = outString;

            return Distance[villeArrive];
        }
		//ordered dictionnary
		
    }
	/// <summary>
	/// This class build a "path" object 
	/// </summary>
	public class PathCity
	{
		private string cityA;//From
        private string cityB;//To
        private int distance;//KM

        public string CityA { get => this.cityA; set => cityA = value; }
		public string CityB { get => this.cityB; set => cityB = value; }
		public int Distance { get => this.distance; set => distance = value; }

        public override string ToString()
        {
            return cityA+" => "+Distance+" => "+cityB; 
        }
    }
    public class PathCityWritter
    {
        private List<PathCity> pathList;
        private HashSet<string> citiesList;
        private int[,] pathMatrice;
        private StreamReader reader = new StreamReader("../../../../TransconnectProject/serializationFiles/Distances.csv");
        private CsvReader csv;
        private string currentPath;//dernier chemin generé

        public PathCityWritter()
        {
            currentPath = "Vide, veuillez generer le chemin";
            csv = new CsvReader(this.reader, CultureInfo.InvariantCulture);

            #region BUILD PATH LIST
            this.pathList = new List<PathCity>();
            var temp = csv.GetRecords<PathCity>().ToList();
            //Permet d'avoir des allers-retours
            foreach (var item in temp)
            {
                PathCity p = new PathCity();
                p.CityA = item.CityB;
                p.CityB = item.CityA;
                p.Distance = item.Distance;
                pathList.Add(item);
                pathList.Add(p);
            }
            #endregion

            #region BUILD CITIES LIST
            this.citiesList = new HashSet<string>();
            foreach (var item in pathList)
            {
                citiesList.Add(item.CityA);
                citiesList.Add(item.CityB);
            }
            #endregion

            #region BUILD MATRICE
            this.pathMatrice = new int[citiesList.Count(), citiesList.Count()];
            for (int i = 0; i < citiesList.Count(); i++)
            {
                for (int j = 0; j < citiesList.Count(); j++)
                {
                    if (citiesList.ElementAt(i) == citiesList.ElementAt(j))
                        pathMatrice[i, j] = 0;
                    else
                    {
                        var finder = pathList.Find(x => x.CityA.Equals(citiesList.ElementAt(i)) && x.CityB.Equals(citiesList.ElementAt(j)));
                        pathMatrice[i, j] = finder != null ? finder.Distance : int.MaxValue;
                    }
                }
            }
            #endregion

        }
        public string CurrentPath { get => this.currentPath; set => this.currentPath = value; }
        public int[,] PathMatrice { get => this.pathMatrice; }
        public List<PathCity> PathList { get => this.pathList; }
        public HashSet<string> CitiesList { get => this.citiesList; }

    }
}

