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
            list = JsonConvert.DeserializeObject<List<Salarie>>(json, converter);
        }
        //Get salarie from Json file (when it's one object file)
        public static Salarie getJsonSalarie(String FilePath, PosteConverter converter)
        {
            StreamReader r = new StreamReader(FilePath);
            string json = @r.ReadToEnd();
            Salarie s = JsonConvert.DeserializeObject<Salarie>(json, converter);
            return s;
        }

        public static void sendJsonSalaries(List<Salarie> list)
        {
            var jsonTest = JsonConvert.SerializeObject(list);
            File.WriteAllText(@"../../../../TransconnectProject/serializationFiles/Salaries.json", jsonTest);
        }



        public static void sendJsonClients(List<Client> list)
        {
            var jsonTest = JsonConvert.SerializeObject(list);
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
    #endregion
}
