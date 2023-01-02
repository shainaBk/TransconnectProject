﻿using Newtonsoft.Json;
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
            else
                return new Poste();
            throw new InvalidOperationException();
        }
    }
    #endregion
}
