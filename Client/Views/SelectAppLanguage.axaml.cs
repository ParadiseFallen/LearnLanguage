using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;

namespace Client.Views
{
    public class SelectAppLanguageView : ReactiveUserControl<SelectAppLanguageViewModel>
    {
        public SelectAppLanguageView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
