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
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class RegisterVM : ReactiveValidationObject, IRoutableViewModel
    {
        private const string EmailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }
        #endregion

        #region Props
        [Reactive]
        public string Username { get; set; }
        [Reactive]
        public string Password { get; set; }
        [Reactive]
        public string RepeatPassword { get; set; }
        [Reactive]
        public string Email { get; set; }
        [Reactive]
        public string Errors { get; set; }
        private AccountAPIService Account { get; }

        #endregion

        #region Commands
        public ReactiveCommand<Unit, Unit> Register { get; }
        public ReactiveCommand<Unit, Unit> GoBack { get; }
        #endregion

        #region IValidatableViewModel
        public ValidationHelper UsernameHelper { get; }
        public ValidationHelper PasswordHelper { get; }
        public ValidationHelper RepeatPasswordHelper { get; }
        public ValidationHelper EmailHelper { get; }
        #endregion

        #region Ctors
        public RegisterVM() { }
        public RegisterVM(IScreen hostScreen)
        {
            #region ctor
            HostScreen = hostScreen;
            Account = Locator.Current.GetService<AccountAPIService>();
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

            RepeatPasswordHelper = this.ValidationRule(
            vm => vm.RepeatPassword,
             p => p is null || p==this.Password,
            "Password must match.");

            EmailHelper = this.ValidationRule(
            vm => vm.Email,
             p => p is null || Regex.IsMatch(p,
             EmailRegex,
             RegexOptions.IgnoreCase),
            "This is not Email.");
            #endregion

            #region Commands
            var canRegister = this.WhenAnyValue(vm=>vm.Username, vm => vm.RepeatPassword, vm => vm.Email, vm => vm.Password, 
                (username,rPassword,email,password) => 
                !string.IsNullOrEmpty(username)&&
                !string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(password) &&
                !string.IsNullOrEmpty(rPassword)&&
                this.ValidationContext.IsValid
                );
            Register = ReactiveCommand.CreateFromTask(
                async () =>
                {
                    var result = await Account.Register(new Login()
                    {
                        Username = Username,
                        Password = Password,
                        Email = Email,
                        Culture = new System.Globalization.CultureInfo("en-EN")
                    });
                    if (string.IsNullOrEmpty(result)) //succses
                        await hostScreen.Router.NavigateAndReset.Execute(new LoginVM(HostScreen) {Username = Username });
                    else
                        Errors = result;
                },
                canExecute: canRegister);

            GoBack = ReactiveCommand.CreateFromTask(async () => 
            { 
                await hostScreen.Router.NavigateBack.Execute(); 
            }, Register.IsExecuting.Select(x => !x));
            #endregion
        }
        #endregion
    }
}
