using ReactiveUI;
using System;
using Avalonia;
using Models.Models;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using Avalonia.Collections;
using Models.Services;
using Splat;
using SharedModels.Models;
using Avalonia.Media.Imaging;
using System.IO;
using Models.Services.API;
using System.Reactive.Linq;
using Client.Services;

namespace Client.ViewModels
{
    public class MainMenuVM : ReactiveObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        #endregion

        #region Comands
        [Reactive]
        public IReactiveCommand Learn { get; set; }
        [Reactive]
        public IReactiveCommand Acount { get; set; }
        [Reactive]
        public IReactiveCommand Exit { get; set; }
        #endregion

        #region ViewProps
        [Reactive]
        public Language FromLanguageSelected { get; set; } = null;
        [Reactive]
        public Language ToLanguageSelected { get; set; } = null;
        [Reactive]
        public AvaloniaList<Language> Languages { get; set; }
        #endregion

        #region PrivateProps
        private LanguageAPIService APIService { get; init; }
        private TranslateAPIService TranslationService { get; init; }

        #endregion

        #region Ctors
        public MainMenuVM()
        {
        }
        public MainMenuVM(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            //resolving
            APIService = Locator.Current.GetService<LanguageAPIService>();
            TranslationService = Locator.Current.GetService<TranslateAPIService>();

            Init();
        }

        public async void Init()
        {
            //init avalible languages
            Languages = new AvaloniaList<Language>(await APIService.GetLanguages());

            Exit = ReactiveCommand.Create(() => { Environment.Exit(0); });
            Acount = ReactiveCommand.Create(() => 
            {
                var cfgSer = Locator.Current.GetService<ILocalSettingsService>();
                cfgSer.Config.AuthToken = null;
                cfgSer.Save();
                HostScreen.Router.NavigateAndReset.Execute(new AuthVM(HostScreen)); 
            });

            Learn = ReactiveCommand.CreateFromTask(async () =>
            {
                await HostScreen.Router.Navigate.Execute(
                    new LearnPhrasesVM(HostScreen, await TranslationService.GetRandomTranslations(
                        FromLanguageSelected.CultureInfo,
                        ToLanguageSelected.CultureInfo,
                        5)) //todo remove count and from
                    );
            },
            canExecute: this
            .WhenAnyValue(
                vm => vm.FromLanguageSelected,
                vm => vm.ToLanguageSelected, (from, to) => from != null && to != null && from != to));
            
            
        }


        #endregion
    }
}
