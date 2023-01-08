using System;
using System.Runtime.Serialization;

namespace TransconnectProject.Util
{
    [DataContract]
    public class Adresse
	{
        [DataMember]
        private string ville;
        [DataMember]
        private string rue;
		public Adresse(string ville, string rue)
		{
			this.ville = ville;
			this.rue = rue;
		}
		public string Ville{
			get => this.ville;
			set
			{
				this.ville = value;
			}
		}
        public override string ToString()
        {
            return this.ville+","+this.rue;
        }
    }
}

