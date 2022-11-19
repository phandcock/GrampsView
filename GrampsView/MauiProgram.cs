using CommunityToolkit.Maui.Core;

using Microsoft.Extensions.Logging;

namespace GrampsView
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            builder.UseMauiApp<App>()
                    .UseMauiCommunityToolkit()
                    .UseMauiCommunityToolkitMarkup()
                    .UseMauiCommunityToolkitCore()
                    .SharedSharpInit()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("fa-solid-900.ttf", "IconFont");
                    });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            MauiApp mauiApp = builder.Build();

            Ioc.Default.ConfigureServices(mauiApp.Services);

            SharedSharp.Common.SharedSharpGeneral.MSAppCenterInit(argMSAppCenterSecretUWP: Common.Secret.UWPSecret, argLogLevel: Microsoft.AppCenter.LogLevel.Error);

            return mauiApp;
        }
    }
}