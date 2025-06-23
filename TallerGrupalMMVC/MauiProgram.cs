
using Microsoft.Extensions.Logging;
using TallerGrupalMMVC.Services;
using TallerGrupalMMVC.ViewModels;
using TallerGrupalMMVC.Views;

namespace TallerGrupalMMVC;

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

        // Servicios
        builder.Services.AddSingleton<GeoLocationServices>();
        builder.Services.AddSingleton<WeatherServices>();

        // ViewModels
        builder.Services.AddTransient<WeatherViewModel>();

        // Views
        builder.Services.AddTransient<WeatherView>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}