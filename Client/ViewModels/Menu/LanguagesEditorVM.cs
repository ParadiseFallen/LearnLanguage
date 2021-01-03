using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels.Menu
{
    public class LanguagesEditorVM : ReactiveObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        #endregion

        public LanguagesEditorVM()
        {

        }

        public LanguagesEditorVM(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

    }
}
