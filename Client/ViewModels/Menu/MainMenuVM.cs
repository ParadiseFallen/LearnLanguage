using ReactiveUI;
using System;
using Avalonia;
using Models;
using ReactiveUI.Fody.Helpers;
using Avalonia.Collections;
using Splat;
using System.Reactive.Linq;
using Client.Services;
using ApiServices.ServicesInterfaces;

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
        private ILanguageService APIService { get; init; }
        private ITranslationService TranslationService { get; init; }

        #endregion

        #region Ctors
        public MainMenuVM()
        {
        }
        public MainMenuVM(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            //resolving
            APIService = Locator.Current.GetService<ILanguageService>();
            TranslationService = Locator.Current.GetService<ITranslationService>();

            Init();
        }

        public async void Init()
        {
            //init avalible languages
            var languageApiResponse = await APIService.GetLanguages();
            Languages = new AvaloniaList<Language>(languageApiResponse.Content);

            Exit = ReactiveCommand.Create(() => { Environment.Exit(0); });
            Acount = ReactiveCommand.Create(async () =>
            {
                var cfgSer = Locator.Current.GetService<ILocalSettingsService>();
                cfgSer.Load().Config.AuthToken = null;
                cfgSer.Save();
                await HostScreen.Router.NavigateAndReset.Execute(new AuthVM(HostScreen));
            });

            Learn = ReactiveCommand.CreateFromTask(async () =>
            {
                var translationApiResponse = await TranslationService.GetRandomTranslations(
                        FromLanguageSelected.CultureInfo,
                        ToLanguageSelected.CultureInfo,
                        5);
                await HostScreen.Router.Navigate.Execute(
                    new LearnPhrasesVM(HostScreen, translationApiResponse.Content) //todo remove count and from
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
