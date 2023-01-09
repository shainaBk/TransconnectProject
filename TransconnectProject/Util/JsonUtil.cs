using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TransconnectProject.Model;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Model.VehiculeModel;
using TransconnectProject.Model.DepartementModel;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TransconnectProject.Util
{
    public class JsonUtil
    {
        //Get all salaries from Json files
        public static void getJsonSalaries(ref List<Salarie> list, PosteConverter converter)
        {
            StreamReader r = new StreamReader(@"../../../../TransconnectProject/serializationFiles/Salaries.json");
            string json = @r.ReadToEnd();
            list = JsonConvert.DeserializeObject<List<Salarie>>(json, new PosteConverter(), new VehiculeConverter(), new DepConverter());
        }
        //Get salarie from Json file (when it's one object file)
        public static Salarie getJsonSalarie(String FilePath, PosteConverter converter)
        {
            StreamReader r = new StreamReader(FilePath);
            string json = @r.ReadToEnd();
            Salarie s = JsonConvert.DeserializeObject<Salarie>(json, new PosteConverter(), new VehiculeConverter(), new DepConverter());
            return s;
        }
        //Salaries parser
        public static void sendJsonSalaries(List<Salarie> list)
        {
            var jsonTest = JsonConvert.SerializeObject(list, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            File.WriteAllText(@"../../../../TransconnectProject/serializationFiles/Salaries.json", jsonTest);
        }
        //Get all Clients from Json files
        public static void getJsonClients(ref List<Client> list)
        {
            StreamReader r = new StreamReader(@"../../../../TransconnectProject/serializationFiles/Clients.json");
            string json = @r.ReadToEnd();
            list = JsonConvert.DeserializeObject<List<Client>>(json,new PosteConverter(),new VehiculeConverter(),new DepConverter());
        }
        //Client parser
        public static void sendJsonClients(List<Client> list)
        {
            var jsonTest = JsonConvert.SerializeObject(list,Formatting.Indented,new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            File.WriteAllText(@"../../../../TransconnectProject/serializationFiles/Clients.json", jsonTest);
        }
    }

    #region Convertisseur Obj
    public abstract class AbstractJsonConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            T target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected static bool FieldExists(
            JObject jObject,
            string name,
            JTokenType type)
        {
            JToken token;
            return jObject.TryGetValue(name, out token) && token.Type == type;
        }
        protected static bool FieldExists(JObject jObject,string name,string value)
        {
            return jObject.GetValue(name).ToString().Equals(value);
        }
    }

    public class PosteConverter : AbstractJsonConverter<Poste>
    {
        protected override Poste Create(Type objectType, JObject jObject)
        {
            if (FieldExists(jObject, "NomPoste", "Chauffeur"))
                return new Chauffeur();
            //It's bullshit
            else if (FieldExists(jObject, "NomPoste", "Chef d'équipe"))
                return new ChefEquipe();
            else if (FieldExists(jObject, "NomPoste", "Directeur des operations"))
                return new DirecteurDesOps();
            else if (FieldExists(jObject, "NomPoste", "Comptable"))
                return new Comptable();
            else if (FieldExists(jObject, "NomPoste", "Commercial"))
                return new Commercial();
            else if (FieldExists(jObject, "NomPoste", "Contrat"))
                return new Contrat();
            else if (FieldExists(jObject, "NomPoste", "Controleur de gestion"))
                return new ControleurDeGestion();
            else if (FieldExists(jObject, "NomPoste", "Directeur commercial"))
                return new DirecteurCommercial();
            else if (FieldExists(jObject, "NomPoste", "Directeur financier"))
                return new DirecteurFinancier();
            else if (FieldExists(jObject, "NomPoste", "Directeur general"))
                return new DirecteurGeneral();
            else if (FieldExists(jObject, "NomPoste", "Directeur RH"))
                return new DirecteurRH();
            else if (FieldExists(jObject, "NomPoste", "Direction comptable"))
                return new DirectionComptable();
            else if (FieldExists(jObject, "NomPoste", "Formation"))
                return new Formation();
            throw new InvalidOperationException();
        }
    }
    public class VehiculeConverter : AbstractJsonConverter<Vehicule>
    {
        protected override Vehicule Create(Type objectType, JObject jObject)
        {
            if (FieldExists(jObject, "Nom", "Voiture"))
                return new Voiture(0);
            //It's bullshit
            else if (FieldExists(jObject, "Nom", "Camion benne"))
                return new Camion(0,null,null);
            else if (FieldExists(jObject, "Nom", "camion-citerne"))
                return new Camion(0, null, null);
            else if (FieldExists(jObject, "Nom", "Camionette"))
                return new Camionette(null);
            else if (FieldExists(jObject, "Nom", "camion frigorifique"))
                return new Camionette(null);
            throw new InvalidOperationException();
        }
    }
    public class DepConverter : AbstractJsonConverter<Departement>
    {
        protected override Departement Create(Type objectType, JObject jObject)
        {
            if (FieldExists(jObject, "NomDep", "Département commercial"))
                return new DepCommercial();
            else if (FieldExists(jObject, "NomDep", "Département des operations"))
                return new DepDesOps();
            else if (FieldExists(jObject, "NomDep", "Département Ressources Humaines"))
                return new DepRH();
            else if (FieldExists(jObject, "NomDep", "Département des Finances"))
                return new DepFinance();
            throw new InvalidOperationException();
        }
    }

    #endregion
}
