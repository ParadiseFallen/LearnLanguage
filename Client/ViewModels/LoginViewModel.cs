using Models.Services.API;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SharedModels.Models;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace Client.ViewModels
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        #endregion

        #region Props
        [Reactive]
        public string Username { get; set; } = "x";
        [Reactive]
        public string Password { get; set; } = "x";
        [Reactive]
        public string Status { get; set; } = "Undefined";
        public IReactiveCommand Login { get; private set; }
        #endregion

        #region Stored services
        private AccountAPIService AccountManager { get; }
        #endregion

        #region Ctors
        public LoginViewModel() { }
        public LoginViewModel(IScreen Host)
        {
            HostScreen = Host;

            AccountManager = Locator.Current.GetService<AccountAPIService>();

            Login = ReactiveCommand.Create(async ()=> 
            {
                Status = (await AccountManager.Login(new Login() 
                {
                    Username = this.Username,
                    Password = this.Password 
                })).ToString();
            });
        }
        #endregion
    }
}
