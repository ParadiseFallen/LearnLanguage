using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;
using Microsoft.AspNetCore.Authorization;
using Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Collections.Generic;

namespace Web.API
{
    [Route("/api/account")]
    public class AccountApi : Controller
    {
        private AccountService Service { get; }
        private UserManager<User> UserManager { get; }

        public AccountApi(AccountService service, UserManager<User> userManager)
        {
            Service = service;
            UserManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ApiResponse<UserInfo>> Login([FromBody]Login data)=>
             await Service.LoginAsync(data);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Login data)
        {
            var errorList = await Service.RegisterAsync(data);
            if (errorList != null)
                return Conflict(new ApiResponse<object>() {Errors= errorList.Select(x => x.Description) });
            var x = Ok(new ApiResponse<object>() { });
            return Ok(new ApiResponse<object>() { });
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await Service.LogoutAsync();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
        [HttpGet]
        public async Task<IList<string>> Info()=>
            await UserManager.GetRolesAsync(await UserManager.GetUserAsync(HttpContext.User));
        
    }
}
