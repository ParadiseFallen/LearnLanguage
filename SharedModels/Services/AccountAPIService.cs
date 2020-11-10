using SharedModels.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Models.Services.API
{
    public class AccountAPIService : APIServiceBase
    {
        public AccountAPIService(string authCookieName,HttpApiClient httpApiClient, JsonSerializerOptions serializerOptions)
            : base(httpApiClient, serializerOptions)
        {
            AuthCookieName = authCookieName;
        }
        public string AuthCookieName { get; }
        public string AuthCookie 
        { 
            get => Http.Cookies.GetCookies(Http.Client.BaseAddress)[AuthCookieName].Value;
            set => Http.Cookies.Add(new System.Net.Cookie(AuthCookieName, value,"/", Http.Client.BaseAddress.Host));
        }

        public async Task<string> Login(Login login)
        {
            try
            {
                var response = await Http.Client.PostAsJsonAsync(APIEndpoints.LoginAccountEndpoint, login);
                return response.IsSuccessStatusCode?"":"Wrong login or password";
            }
            catch (Exception)
            {
                return "Server not responding";
            }
        }

        public async Task<string> Logout()
        {
            try
            {
                await Http.Client.PostAsJsonAsync(APIEndpoints.LogoutAccountEndpoint, SerializerOptions);
                return "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "Server not responding";
            }
        }
        public async Task<string> Register(Login login)
        {
            try
            {
                var response = await Http.Client.PostAsJsonAsync(APIEndpoints.RegisterAccountEndpoint, login,SerializerOptions);
                if (response.IsSuccessStatusCode)
                    return "";
                return (await response.Content.ReadFromJsonAsync<IEnumerable<string>>(SerializerOptions)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "Server not responding";
            }
        }

    }
}
