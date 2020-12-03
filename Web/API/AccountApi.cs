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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] Login data)
        {

            return null;
            //if (await Service.LoginAsync(data))
            //    return Ok();
            //return Unauthorized("Wrong login or password");
        }

        [HttpPost("register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] Login data)
        {
            var errorList = await Service.RegisterAsync(data);
            if (errorList != null)
                return Conflict(errorList.Select(x => x.Description));
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
        [HttpPost("logout")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await Service.LogoutAsync();
            return Ok();
        }
    }
}
