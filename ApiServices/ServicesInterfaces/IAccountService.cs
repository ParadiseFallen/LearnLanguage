using ApiClient;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.ServicesInterfaces
{
    public interface IAccountService
    {
        public RestClient RestClient { get; }
        public Task<ApiResponse<UserInfo>> Login(Login login);
        public Task<ApiResponse<bool>> Logout();
        public Task<ApiResponse<bool>> Register(Login login);
        public string AuthCookie { get; set; }
    }
}
