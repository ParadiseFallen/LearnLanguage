using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace System.Text.Json
{
    public static class JsonExtension
    {
        public static IEnumerable<JsonConverter> GetAllConverters()
        {
            var list = new List<JsonConverter>();
            typeof(JsonExtension)
                    .Assembly
                    .GetTypes()
                    .Where(type => !type.IsAbstract && type.Namespace.Contains(nameof(Models.JsonConverters)))
                    .ToList().ForEach(type =>
                    {
                        list.Add((JsonConverter)type.GetConstructor(Type.EmptyTypes).Invoke(null));
                    });
            return list;
        }
        public static void AddAllConverters(this JsonSerializerOptions jsonOptions)
        {
            foreach (var item in GetAllConverters())
            {
                jsonOptions.Converters.Add(item);
            }
            
        }

    }
}
