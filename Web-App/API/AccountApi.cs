using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModels.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

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
            if (await Service.LoginAsync(data))
                return Ok();
            return Unauthorized("Wrong login or password");
        }

        [HttpPost("register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] Login data)
        {
            var errors = await Service.RegisterAsync(data);
            if (errors == null)
            {
                return Ok();
            }
            return Conflict(errors);
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
