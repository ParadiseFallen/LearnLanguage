using ApiClient;
using ApiServices.Endpoints;
using ApiServices.ServicesInterfaces;
using Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiServices.Services
{
    public class AccountService : RestApiServiceBase, IAccountService
    {
        public AccountService(RestClient restClient) : base(restClient) { }


        private Func<Exception, Task<ApiResponse<UserInfo>>> ExceptionHandlerUserInfo =>
            new Func<Exception, Task<ApiResponse<UserInfo>>>(async ex =>
        {
            if (ex is HttpRequestException)
                return new ApiResponse<UserInfo>() { Errors = new[] { "Server not responding." } };
            return new ApiResponse<UserInfo>() { Errors = new[] { "Unhandled exception!" } };
        });

        public Task<ApiResponse<UserInfo>> Login(Login login) =>
            CreateRequest<ApiResponse<UserInfo>>(PostAsync(Api.Account.Login, login))
            .SetExceptionHandler(ExceptionHandlerUserInfo)
            .ExecuteRequestAsync();

        public Task<ApiResponse<bool>> Logout() =>
            CreateRequest<ApiResponse<bool>>(PostAsync(Api.Account.Logout))
            .SetResponseHandler(async respone => new ApiResponse<bool>(null, respone.IsSuccessStatusCode))
            .ExecuteRequestAsync();

        public Task<ApiResponse<bool>> Register(Login user) =>
            CreateRequest<ApiResponse<bool>>(PostAsync(Api.Account.Register, user))
            .SetResponseHandler(
                async response =>
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>(SerializerOptions);
                    return new ApiResponse<bool>() { Content = response.IsSuccessStatusCode, Errors = apiResponse.Errors };
                })
            .SetExceptionHandler(
                async ex =>
                {
                    if (ex is HttpRequestException)
                        return new ApiResponse<bool> { Errors = new[] { "Server not responding." }, Content = false };
                    return new ApiResponse<bool> { Errors = new[] { "Unhandled exception!" }, Content = false };
                })
            .ExecuteRequestAsync();
    }
}
