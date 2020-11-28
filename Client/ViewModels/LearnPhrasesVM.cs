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
        public Translation CurrentTranslation { get; private set; }
        private IEnumerable<Translation> Translations { get; init; }
        #endregion

        #region Commands
        [Reactive]
        public IReactiveCommand MoveNext { get; set; }
        public IReactiveCommand Exit { get; set; }
        #endregion

        #region Ctors
        public LearnPhrasesVM(IScreen hostScreen, IEnumerable<Translation> translations)
        {
            HostScreen = hostScreen;
            Translations = translations;
            Exit = ReactiveCommand.Create(() => { HostScreen.Router.NavigateBack.Execute(); });
            Init();
        }

        private async void Init()
        {
            ////get translations
            //Translations = await TranslateService.GetRandomTranslations(BaseLanguage.CultureInfo, TargetLanguage.CultureInfo, 5);

            ////create move next
            var enumerator = Translations.GetEnumerator();
            enumerator.MoveNext();
            CurrentTranslation = enumerator.Current;
            MoveNext = ReactiveCommand.Create(() =>
            {
                if (enumerator.MoveNext())
                    CurrentTranslation = enumerator.Current;
                else
                    HostScreen.Router.Navigate.Execute(new ExamPhrasesVM(HostScreen, Translations));
                //Console.WriteLine(CurrentTranslation.B.Text);
            });
        }
        #endregion

    }
}
