using SharedModels.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;
using System.Diagnostics;

namespace Models.Services.API
{
    public class AccountAPIService : APIServiceBase
    {
        public AccountAPIService(HttpApiClient httpApiClient, JsonSerializerOptions serializerOptions)
            : base(httpApiClient, serializerOptions)
        {
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
