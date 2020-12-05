using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using Models;
using Splat;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Client.Services;
using ApiServices.ServicesInterfaces;
using ApiServices.Extensions;

namespace Client.ViewModels
{
    public class LoginVM : ReactiveValidationObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        #endregion

        #region Props
        private IAccountService Account { get; }
        private ILocalSettingsService ConfigProvider { get; }
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
        public ReactiveCommand<Unit,ApiResponse<UserInfo>> Login { get; private set; }
        public IReactiveCommand Register { get; private set; }

        #endregion

        #region IValidatableViewModel
        public ValidationHelper UsernameHelper { get; }
        public ValidationHelper PasswordHelper { get; }
        #endregion

        #region Ctors
#if DEBUG
        public LoginVM() { }
#endif
        public LoginVM(IScreen Host)
        {
            #region Base init
            HostScreen = Host;
            Account = Locator.Current.GetService<IAccountService>();
            ConfigProvider = Locator.Current.GetService<ILocalSettingsService>();
            Username = ConfigProvider.Config.Username;
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
            Login = ReactiveCommand.CreateFromTask<Unit,ApiResponse<UserInfo>>(
            async _ => await Account.Login(new Login()
            {
                    Username = this.Username,
                    Password = this.Password
            }),
            canExecute: 
            this.WhenAnyValue(
                vm => vm.Password,
                vm => vm.Username, 
                (p, u) => !string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(u)));

            //when login completed
            Login.Subscribe(async apiResponse => 
            {
                if (apiResponse.Content != null)
                {
                    if (RememberMe)
                    {
                        ConfigProvider.Config.Username = Username;
                        ConfigProvider.Config.AuthToken = Account.AuthCookie;
                        ConfigProvider.Save();
                    }
                    var main = HostScreen as AuthVM;
                    await main.HostScreen.Router.Navigate.Execute(new MainMenuVM(HostScreen));
                }
                else
                {
                    Status = apiResponse.Errors.FirstOrDefault();
                }
            });

            Register = ReactiveCommand.Create(
                () => HostScreen.Router.Navigate.Execute(new RegisterVM(HostScreen) { Username = Username }),
                canExecute: Login.IsExecuting.Select(x=>!x));
            #endregion
        }
        #endregion
    }
}
