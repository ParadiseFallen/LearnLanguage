using System;
using System.Text.Json;

namespace Models.Services.API
{
    public abstract class APIServiceBase
    {
        public HttpApiClient Http { get; private set; }
        public JsonSerializerOptions SerializerOptions { get; private set; }
        public APIServiceBase(HttpApiClient httpApiClient, JsonSerializerOptions serializerOptions)
        {
            if (httpApiClient == null)
                throw new NullReferenceException($"HttpApiClient is null");
            SerializerOptions = serializerOptions;
            Http = httpApiClient;
        }
    }
}
