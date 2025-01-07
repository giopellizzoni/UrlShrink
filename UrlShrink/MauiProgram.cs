using Microsoft.Extensions.Logging;

using UrlShrink.Services;
using UrlShrink.Services.Interfaces;
using UrlShrink.ViewModels;

namespace UrlShrink;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddTransient<IURLService, URLService>();
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<MainPage>(sp => new MainPage(sp.GetRequiredService<MainPageViewModel>()));


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
