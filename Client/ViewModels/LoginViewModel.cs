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
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

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
        public string Status { get; set; } = "Undefined";
        #endregion

        #region Commands
        public IReactiveCommand Login { get; private set; }
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
            #endregion

            #region Validation
            UsernameHelper = this.ValidationRule(
            vm => vm.Username,
            name => 
            {
                if (name == null)
                    return false;
                return name.Trim() == "";
            },
            "Username shouldn't be empty.");

            PasswordHelper = this.ValidationRule(
            vm => vm.Password,
            p => 
            {
                if (p == null)
                    return false;
                return p.Trim() == "";
            },
            "Password shouldn't be empty.");
            PasswordHelper.DelayChangeNotifications().Dispose();
            #endregion

            #region Commands
            Login = ReactiveCommand.CreateFromTask(
            async () =>
            {
                //Status = (await AccountManager.Login(new Login() 
                //{
                //    Username = this.Username,
                //    Password = this.Password 
                //})).ToString();
                await Task.Delay(5000);
            },
            canExecute: this.IsValid());
            
            Register = ReactiveCommand.Create(() => HostScreen.Router.Navigate.Execute(new RegisterViewModel(HostScreen)));
            #endregion
        }
        #endregion
    }
}
