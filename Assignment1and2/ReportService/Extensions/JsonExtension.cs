using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ReportService.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson(this object objectToSerialize)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> {
                    new IsoDateTimeConverter
                    {
                        DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.FFFZ"
                    }
                }
            };

            return JsonConvert.SerializeObject(objectToSerialize, serializerSettings);
        }
    }
}

