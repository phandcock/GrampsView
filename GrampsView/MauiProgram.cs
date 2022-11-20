using CommunityToolkit.Maui.Markup;

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
                    .SharedSharpInit()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("fa-solid-900.ttf", "IconFont");
                    });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddLocalization();

            MauiApp mauiApp = builder.Build();

            Ioc.Default.ConfigureServices(mauiApp.Services);

            SharedSharp.Common.SharedSharpGeneral.MSAppCenterInit(argMSAppCenterSecretAndroid: Common.Secret.AndroidSecret, argMSAppCenterSecretWinUI: Common.Secret.UWPSecret, argLogLevel: Microsoft.AppCenter.LogLevel.Error);

            return mauiApp;
        }
    }
}