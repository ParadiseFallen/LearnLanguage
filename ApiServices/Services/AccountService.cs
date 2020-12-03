using ApiClient;
using ApiServices.Endpoints;
using ApiServices.ServicesInterfaces;
using Models;
using System.Threading.Tasks;

namespace ApiServices.Services
{
    public class AccountService : RestApiServiceBase, IAccountService
    {
        public AccountService(RestClient restClient) : base(restClient) { }

        public Task<ApiResponse<User>> Login(Login login) =>
            ExecuteRequestAsync<ApiResponse<User>>(PostAsync(Api.Account.Login,login));
        public Task<ApiResponse<bool>> Logout() =>
            ExecuteRequestAsync(
                PostAsync(Api.Account.Logout),
                async respone=>new ApiResponse<bool>(null,respone.IsSuccessStatusCode)
                );
        public Task<ApiResponse<User>> Register(Login user) =>
            ExecuteRequestAsync<ApiResponse<User>>(PostAsync(Api.Account.Login, user));

    }
}
