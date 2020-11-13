using Client.Models;
using Client.Services;
using DynamicData;
using Models.Models;
using Models.Services.API;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SharedModels.Models;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class LearnPhrasesVM : ReactiveObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        #endregion


        #region Props
        [Reactive]
        public Translation Translation { get; private set; }
        private IEnumerable<Translation> Translations { get; set; }
        private TranslateAPIService TranslateService { get; }
        private ILocalSettingsService ConfigService { get; }
        private Language BaseLanguage { get; }
        private Language TargetLanguage { get; }

        #endregion

        #region Commands
        public IReactiveCommand MoveNext { get; set; }
        public IReactiveCommand Exit {get; set; }
        #endregion

        #region Ctors
        public LearnPhrasesVM(IScreen hostScreen,Language baseLanguage,Language target)
        {
            HostScreen = hostScreen;
            TranslateService = Locator.Current.GetService<TranslateAPIService>();
            ConfigService = Locator.Current.GetService<ILocalSettingsService>();
            BaseLanguage = baseLanguage;
            TargetLanguage = target;
            Init();
        }

        private async void Init()
        {
            //get translations
            Translations = await TranslateService.GetRandomTranslations(BaseLanguage.CultureInfo, TargetLanguage.CultureInfo, 5);
            
            //create move next
            var enumerator = Translations.GetEnumerator();
            MoveNext = ReactiveCommand.Create(() =>
            {
                if (enumerator.MoveNext())
                    Translation = enumerator.Current;
                else
                    HostScreen.Router.Navigate.Execute(new ExamPhrasesVM(HostScreen, Translations));
            });
        }
        #endregion

    }
}
