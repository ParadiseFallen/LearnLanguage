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
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class RegisterViewModel : ReactiveValidationObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }
        #endregion

        #region props
        [Reactive]
        public string Username { get; set; }
        [Reactive]
        public string Password { get; set; }
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
        #endregion

        #region Ctors
        public RegisterViewModel() { }
        public RegisterViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            Account = Locator.Current.GetService<AccountAPIService>();
            #region Validation
            UsernameHelper = this.ValidationRule(
            vm => vm.Username,
            name => !string.IsNullOrEmpty(name),
            "Username shouldn't be empty.");

            PasswordHelper = this.ValidationRule(
            vm => vm.Password,
            p => !string.IsNullOrEmpty(p),
            "Password shouldn't be empty.");
            #endregion

            Register = ReactiveCommand.CreateFromTask(
                async unit=>
                {
                    var result = await Account.Register(new Login()
                    {
                        Username = Username,
                        Password = Password
                    });
                    if (string.IsNullOrEmpty(result)) //succses
                        await hostScreen.Router.NavigateBack.Execute();
                    else
                        Errors = result;
                },
                canExecute: this.IsValid());

            GoBack = ReactiveCommand.CreateFromTask(async ()=> { await hostScreen.Router.NavigateBack.Execute(); },Register.IsExecuting.Select(x=>!x));
        }
        #endregion
    }
}
