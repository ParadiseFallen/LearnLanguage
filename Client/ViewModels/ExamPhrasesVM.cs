using ReactiveUI;
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

        public ExamPhrasesVM(IScreen hostScreen,IEnumerable<Translation> translations)
        {
            HostScreen = hostScreen;

        }
    }
}
