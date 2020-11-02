using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
                    .Where(type => !type.IsAbstract && type.Namespace.Contains(nameof(SharedModels.JsonConverters)))
                    .ToList().ForEach(type =>
                    {
                        list.Add((JsonConverter)type.GetConstructor(Type.EmptyTypes).Invoke(null));
                    });
            return list;
        }
        public static void AddAllConverters(this JsonOptions jsonOptions)
        {
            foreach (var item in GetAllConverters())
            {
                jsonOptions.JsonSerializerOptions.Converters.Add(item);
            }
            
        }

    }
}
