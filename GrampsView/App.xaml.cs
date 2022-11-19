using Microsoft.Extensions.DependencyInjection;

using SharedSharp.Common;
using SharedSharp.Common.Interfaces;

using SharedSharpTest.Common;
using SharedSharpTest.Services;

using System;

using Xamarin.Forms;

namespace SharedSharpTest
{
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            ShardSharpCore.InitService(Services);

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        public static new App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        protected override void OnResume()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnStart()
        {
            SharedSharpGeneral.MSAppCenterInit(argMSAppCenterSecretUWP: "a5dfe676-e0ec-49f3-aed6-86c73b82ad1d", argLogLevel: Microsoft.AppCenter.LogLevel.Error);

            ShardSharpCore.Init(Microsoft.Extensions.Logging.LogLevel.Trace);
        }

        private static IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            _ = services.AddSingleton<ISharedSharpAppInit, AppInit>();

            ShardSharpCore.InitServicesAdd(ref services);

            return services.BuildServiceProvider();
        }
    }
}