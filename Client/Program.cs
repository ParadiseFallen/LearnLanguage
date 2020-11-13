using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls.ApplicationLifetimes;
using Client;

#if NET5_0 //real build config
// Initialization code. Don't use any Avalonia, third-party APIs or any
// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
// yet and stuff might break.
BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

// Avalonia configuration, don't remove; also used by visual designer.
AppBuilder BuildAvaloniaApp()
   => AppBuilder.Configure<App>()
       .UsePlatformDetect()
       .LogToTrace()
       .RegisterInjections()
       .UseReactiveUI();

//support for avalonia designer
#else
namespace Client
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .RegisterInjections()
                .UseReactiveUI();
    }


}
#endif
