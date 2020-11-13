using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services.API
{
    public class HttpApiClient : IDisposable
    {
        public CookieContainer Cookies { get; protected set; }
        public HttpClientHandler Handler { get; protected set; }
        public HttpClient Client { get; protected set; }
        public string AuthCookieName { get;}
        public string AuthToken
        {
            get => Cookies.GetCookies(Client.BaseAddress)[AuthCookieName].Value;
            set => Cookies.Add(new Cookie(AuthCookieName, value, "/", Client.BaseAddress.Host));
        }
        
        public HttpApiClient(Uri baseURI,string authCookieName = ".AspNetCore.Identity.Application")
        {
            AuthCookieName = authCookieName;
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
        public async Task<bool> IsActive()
        {
            var t = await Client.GetAsync(APIEndpoints.APIEndpoint);
            return t.IsSuccessStatusCode;
        }
    }
}
