using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ExamPhrasesVM : ReactiveObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        #endregion

        #region Props
        [Reactive]
        public Translation CurrentTranslation { get; private set; }
        private IEnumerable<Translation> Translations { get; init; }

        [Reactive]
        public string AText { get; set; } = "";
        [Reactive]
        public string CorrectMarker { get; set; }
        [Reactive]
        public bool CanInput { get; set; } = true;
        [Reactive]
        public bool IsMoveNextAvable { get; set; }
        #endregion

        #region Commands
        [Reactive]
        public IReactiveCommand MoveNext { get; set; }
        public IReactiveCommand Check { get; set; }
        public IReactiveCommand Exit { get; set; }

        #endregion

        public ExamPhrasesVM(IScreen hostScreen,IEnumerable<Translation> translations)
        {
            HostScreen = hostScreen;
            Translations = translations;
            
            var enumerator = Translations.GetEnumerator();
            enumerator.MoveNext();
            CurrentTranslation = enumerator.Current;

            Check = ReactiveCommand.Create(()=> 
            {
                CanInput = false;
                if (TrimOptional(CurrentTranslation.A.Text).Equals(TrimOptional(AText)))
                    CorrectMarker = "Correct. Well done.";
                else
                {
                    CorrectMarker = "Not correct. Try again.";
                    AText = $"Correct is : {CurrentTranslation.A.Text}";
                }
                IsMoveNextAvable = true;
            });

            MoveNext = ReactiveCommand.Create(() => 
            {
                if (enumerator.MoveNext())
                    CurrentTranslation = enumerator.Current;
                else
                    HostScreen.Router.NavigateAndReset.Execute(new MainMenuVM(HostScreen));

                CanInput = true;
                CorrectMarker = AText = string.Empty;
                IsMoveNextAvable = false;
            });

            Exit = ReactiveCommand.Create(() => HostScreen.Router.NavigateAndReset.Execute(new MainMenuVM(HostScreen)));
        }

        

        private string TrimOptional(string data) => data.ToLower().Trim().Replace(".", "").Replace("  ", "").Replace("!", "");

    }
}
