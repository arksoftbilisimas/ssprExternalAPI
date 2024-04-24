using Newtonsoft.Json;

namespace SSPRExternalApiExample.Api.Helpers
{
    public class JsonHelper : Singleton<JsonHelper>
    {
        public T DeserizalizeObject<T>(object obj)
        {
            return JsonConvert.DeserializeObject<T>(Convert.ToString(obj), new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public string SerializeObject(object obj, bool isCamelCase = false)
        {
            if (isCamelCase)
            {
                return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });
            }
            else
            {
                return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
        }
    }
}
