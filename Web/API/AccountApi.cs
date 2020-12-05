using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;
using Microsoft.AspNetCore.Authorization;
using Models;

namespace Web.API
{
    [Route("/api/account")]
    public class AccountApi : Controller
    {
        private AccountService Service { get; }
        public AccountApi(AccountService service)
        {
            Service = service;
        }

        [HttpPost("login")]
        public async Task<ApiResponse<UserInfo>> Login([FromBody] Login data)=>
             await Service.LoginAsync(data);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Login data)
        {
            var errorList = await Service.RegisterAsync(data);
            if (errorList != null)
                return Conflict(new ApiResponse<object>() {Errors= errorList.Select(x => x.Description) });
            return Ok(new ApiResponse<object>() { });
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await Service.LogoutAsync();
            return Ok();
        }
    }
}
