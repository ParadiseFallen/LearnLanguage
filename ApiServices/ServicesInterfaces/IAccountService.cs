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
        Task<ApiResponse<User>> Login(Login login);
        Task<ApiResponse<bool>> Logout();
        Task<ApiResponse<User>> Register(Login login);
    }
}
