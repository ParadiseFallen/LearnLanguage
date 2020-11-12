using Microsoft.AspNetCore.Identity;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Services;

namespace Web.Services
{
    public class AccountService : DBService
    {
        private SignInManager<User> SigninManager { get; }
        private UserManager<User> UserManager { get; }
        public AccountService(DatabaseContext db, SignInManager<User> signInManager, UserManager<User> userManager ) : base(db)
        {
            SigninManager = signInManager;
            UserManager = userManager;
        }


        public async Task<bool> LoginAsync(Login login)
        {
            SignInResult result = await SigninManager.PasswordSignInAsync(login.Username, login.Password, false, false);
            return result.Succeeded;
        }
        public async Task LogoutAsync()
        {
            await SigninManager.SignOutAsync();
        }
        public async Task<IEnumerable<IdentityError>> RegisterAsync(Login login)
        {
            //var lang = 
            var createdUser = await UserManager.CreateAsync(new User() 
            { 
                UserName = login.Username,
                Email=login.Email,
                //NativeCulture  
            }, login.Password);

            if (createdUser.Succeeded)
                return null;

            return createdUser.Errors;
        }
        //public async Task<bool> UpdateUser()
        //{

        //}
    }
}
