using Models.Services.API;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using SharedModels.Models;
using Splat;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Diagnostics;
using System.Configuration;
using Client.Services;

namespace Client.ViewModels
{
    public class LoginViewModel : ReactiveValidationObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        #endregion

        #region Props
        private AccountAPIService Account { get; }
        [Reactive]
        public string Username { get; set; }
        [Reactive]
        public string Password { get; set; }
        [Reactive]
        public string Status { get; set; }
        [Reactive]
        public bool RememberMe { get; set; }
        #endregion

        #region Commands
        public ReactiveCommand<Unit,string> Login { get; private set; }
        public IReactiveCommand Register { get; private set; }

        #endregion

        #region IValidatableViewModel
        public ValidationHelper UsernameHelper { get; }
        public ValidationHelper PasswordHelper { get; }
        #endregion

        #region Ctors
        public LoginViewModel() { }
        public LoginViewModel(IScreen Host)
        {
            #region Base init
            HostScreen = Host;
            Account = Locator.Current.GetService<AccountAPIService>();
            var prefs = PreferenceService.GetOrCreate();
            if (prefs!= default)
            {
                Username = Username = prefs.Username;
            }
           
            #endregion

            #region Validation
            UsernameHelper = this.ValidationRule(
            vm => vm.Username,
            name => name is null || name.Trim().Length > 0,
            "Username shouldn't be empty.");

            PasswordHelper = this.ValidationRule(
            vm => vm.Password,
             p => p is null || p.Trim().Length > 0,
            "Password shouldn't be empty.");
            #endregion

            #region Commands
            Login = ReactiveCommand.CreateFromTask<Unit,string>(
            async _ =>
            {
                return await Account.Login(new Login()
                {
                    Username = this.Username,
                    Password = this.Password
                });
            },
            canExecute: this.IsValid());

            Login.Subscribe(x => 
            {
                Status = x;
                if (RememberMe)
                {
                    prefs.Username = Username;
                    PreferenceService.SaveLocalStore(prefs);
                }
            });

            //Account.AuthCookie = "test";

            Register = ReactiveCommand.Create(
                () => HostScreen.Router.Navigate.Execute(new RegisterViewModel(HostScreen) { Username = Username }),
                canExecute: Login.IsExecuting.Select(x=>!x));
            #endregion
        }
        #endregion
    }
}
