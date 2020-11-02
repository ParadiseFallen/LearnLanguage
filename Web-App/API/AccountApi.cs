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
        private SignInManager<User> signinManager;
        private UserManager<User> userManager;
        public AccountApi(SignInManager<User> manager, UserManager<User> userManager)
        {
            signinManager = manager;
            this.userManager = userManager;
        }

        [HttpPost("login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] Login data)
        {
            Microsoft.AspNetCore.Identity.SignInResult result
            = await signinManager.PasswordSignInAsync(data.Username,
            data.Password, false, false);
            if (result.Succeeded)
                return Ok();

            return Unauthorized("Wrong login or password");
        }

        [HttpPost("register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] Login data)
        {
            var createdUser = await userManager.CreateAsync(new User() {UserName = data.Username }, data.Password);
            if (createdUser.Succeeded)
            {
                return Ok();
            }
            return Conflict(createdUser.Errors);
        }

        [HttpPost("logout")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signinManager.SignOutAsync();
            return Ok();
        }
    }
}
