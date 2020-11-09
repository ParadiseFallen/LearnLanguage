using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;

namespace Models.Services.API
{
    public class AccountAPIService : APIServiceBase
    {
        public AccountAPIService(HttpApiClient httpApiClient, JsonSerializerOptions serializerOptions)
            : base(httpApiClient, serializerOptions)
        {
        }

        public async Task<bool> Login(Login login)
        {
            var response = await Http.Client.PostAsJsonAsync(APIEndpoints.LoginAccountEndpoint, login);
            return response.IsSuccessStatusCode;
        }

        public async Task Logout()
        {
            await Http.Client.PostAsJsonAsync(APIEndpoints.LogoutAccountEndpoint, SerializerOptions);
        }
        public async Task<string> Register(Login login)
        {
            try
            {
                var response = await Http.Client.PostAsJsonAsync(APIEndpoints.RegisterAccountEndpoint, login);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                return "Server not responding";
            }
        }
    }
}
