using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class AccountService : DBService
    {
        private SignInManager<User> SigninManager { get; }
        private UserManager<User> UserManager { get; }
        public AccountService(DatabaseContext db, SignInManager<User> signInManager, UserManager<User> userManager) : base(db)
        {
            SigninManager = signInManager;
            UserManager = userManager;
        }

        public async Task<ApiResponse<UserInfo>> LoginAsync(Login login)
        {
            var result =  await SigninManager.PasswordSignInAsync(login.Username, login.Password, false, false);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByNameAsync(login.Username);
                return new ApiResponse<UserInfo>() { Content = new UserInfo() 
                {
                    Username = user.UserName,
                    Email=user.Email,
                    NativeCulture = user.NativeCulture 
                } }; //set user
            }
            return new ApiResponse<UserInfo>() { Errors = new[] { "Wrong login or password." } };
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
    }
}
