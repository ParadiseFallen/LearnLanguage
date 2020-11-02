using System;

namespace Models.Services.API
{
    public abstract class APIServiceBase
    {
        public HttpApiClient Http { get; private set; }
        public APIServiceBase(HttpApiClient httpApiClient)
        {
            if (httpApiClient == null)
                throw new NullReferenceException($"HttpApiClient is null");
            Http = httpApiClient;
        }
    }
}
