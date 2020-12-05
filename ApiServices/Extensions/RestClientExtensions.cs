using ApiClient;
using System;
using System.Net;

namespace ApiServices.Extensions
{
    public static class RestClientExtensions
    {
        public static string AuthCookieName { get; set; } = ".AspNetCore.Identity.Application";

        public static void SetAuthCookie(this RestClient client, string data) =>
            client.Handler.CookieContainer.Add(new Cookie(AuthCookieName, data, "/"));

        public static string GetAuthCookie(this RestClient client) =>
            client.Handler.CookieContainer.GetCookies(new Uri("/"))[AuthCookieName].Value;
    }
}
