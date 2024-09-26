using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;
using BingMaui.Data.Interfaces;
using BingMaui.Data.Services;
using BingMaui.ViewModels;
using BingMaui.Pages;
using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;

namespace BingMaui {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseBarcodeReader()
                .ConfigureSyncfusionCore();

            builder.Services.AddSingleton<IAuthService, AuthService>();

            builder.Services.AddSingleton<ShakeDetectorPage>();
            builder.Services.AddSingleton<ShakeDetectorViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
