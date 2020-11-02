using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Models.Services.API
{
    public class HttpApiClient : IDisposable
    {
        public CookieContainer Cookies { get; protected set; }
        public HttpClientHandler Handler { get; protected set; }
        public HttpClient Client { get; protected set; }
        public HttpApiClient(Uri baseURI) 
        {
            if (baseURI == null)
                throw new NullReferenceException($"Base URI undefined!");

            Cookies = new CookieContainer();
            Handler = new HttpClientHandler() { CookieContainer = Cookies };
            Client = new HttpClient(Handler) { BaseAddress = baseURI };
        }
        public void Dispose()
        {
            Handler.Dispose();
            Client.Dispose();
        }
    }
}
