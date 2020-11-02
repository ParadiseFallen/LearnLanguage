using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
namespace Models.Services.API
{
    public class AccountAPIService : APIServiceBase
    {
        public AccountAPIService(HttpApiClient httpApiClient) : base(httpApiClient) { }

        public async Task<bool> Login(Login login)
        {
            var response = await Http.Client.PostAsJsonAsync(APIEndpoints.LoginAccountEndpoint,login);
            return response.IsSuccessStatusCode;
        }

        public async Task Logout()
        {
            await Http.Client.PostAsync(APIEndpoints.LogoutAccountEndpoint,null);
        }
        public async Task<bool> Register(Login login)
        {
            var response = await Http.Client.PostAsJsonAsync(APIEndpoints.RegisterAccountEndpoint, login);
            return response.IsSuccessStatusCode;
        }
    }
}
