using GrampsView.Common;
using GrampsView.Data;
using GrampsView.Data.External.StoreSerial;
using GrampsView.Data.ExternalStorage;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.ViewModels;
using GrampsView.ViewModels.MinorPages;
using GrampsView.ViewModels.StartupPages;

using Microsoft.AppCenter;
using Microsoft.Extensions.DependencyInjection;

using SharedSharp.Common;
using SharedSharp.Common.Interfaces;
using SharedSharp.Errors.Interfaces;
using SharedSharp.Services;
using SharedSharp.Services.Interfaces;

using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace GrampsView
{
    public sealed partial class App : Application
    {
        private static readonly HLinkFamilyModel FamilyStartModel = null;

        private static readonly HLinkPersonModel PersonStartPage = null;

        public App()
        {
            Services = ConfigureServices();
            ShardSharpCore.InitService(Services);

            InitializeComponent();

            MainPage = new AppShell();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public static new App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        protected override void OnAppLinkRequestReceived(System.Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);
        }

        protected override void OnResume()
        {
            Debug.WriteLine("Resuming");

            base.OnResume();

            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("Sleeping");

            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            // This lookup NOT required for Windows platforms - the Culture will be automatically set
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                // Determine the correct, supported .NET culture
                _ = DependencyService.Get<ILocalize>();

                CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

                // Assets.Strings.AppResources.Culture = ci; TODO set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            // Setup various support frameworks
            // Any updates?
            SharedSharpGeneral.MSAppCenterInit(argMSAppCenterSecretAndroid: Secret.AndroidSecret, argMSAppCenterSecretIOS: Secret.IOSSecret, argMSAppCenterSecretUWP: Secret.UWPSecret, argLogLevel: LogLevel.Verbose);

            ShardSharpCore.Init(argGlobalDebugFlag: true);

            VersionTracking.Track();

            _ = Services.GetService<IErrorNotifications>();

            _ = Services.GetService<IDataRepositoryManager>();

            // App Setup
            Application.Current.UserAppTheme = SharedSharpSettings.ApplicationTheme;

            SharedSharpSettings.DatabaseVersionMin = Constants.GrampsViewDatabaseVersion;

            // Get Going
            StartAtDetailPage().GetAwaiter().GetResult();

            if (DataStore.Instance.DS.IsDataLoaded)
            {
                SharedSharpNavigation.NavigateHub();

                return;
            }

            // Get Going
            _ = Services.GetService<ISharedSharpAppInit>().Init().ConfigureAwait(false);
        }

        private static IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            // Add Services
            _ = services.AddSingleton<ISharedSharpAppInit, AppInit>();
            _ = services.AddSingleton<IDatabaseReloadDisplayService, DatabaseReloadDisplayService>();
            _ = services.AddSingleton<IDataRepositoryManager, DataRepositoryManager>();
            _ = services.AddSingleton<IGrampsStoreSerial, GrampsStoreSerial>();
            _ = services.AddSingleton<IStoreFile, StoreFile>();
            _ = services.AddSingleton<IStorePostLoad, StorePostLoad>();
            _ = services.AddSingleton<IStoreXML, StoreXML>();

            // Viewmodels
            _ = services.AddTransient<AboutViewModel>();
            _ = services.AddTransient<AddressDetailViewModel>();
            _ = services.AddTransient<AttributeDetailViewModel>();

            _ = services.AddTransient<BookMarkListViewModel>();

            _ = services.AddTransient<ChildRefDetailViewModel>();
            _ = services.AddTransient<CitationDetailViewModel>();
            _ = services.AddTransient<CitationListViewModel>();

            _ = services.AddTransient<DateRangeDetailViewModel>();
            _ = services.AddTransient<DateSpanDetailViewModel>();
            _ = services.AddTransient<DateStrDetailViewModel>();
            _ = services.AddTransient<DateValDetailViewModel>();

            _ = services.AddTransient<EventDetailViewModel>();
            _ = services.AddTransient<EventListViewModel>();

            _ = services.AddTransient<FamilyDetailViewModel>();
            _ = services.AddTransient<FamilyListViewModel>();
            _ = services.AddTransient<FileInputHandlerViewModel>();
            _ = services.AddTransient<FirstRunViewModel>();

            _ = services.AddTransient<HubViewModel>();

            _ = services.AddTransient<MediaDetailViewModel>();
            _ = services.AddTransient<MediaListViewModel>();

            _ = services.AddTransient<NeedDatabaseReloadViewModel>();
            _ = services.AddTransient<NoteDetailViewModel>();
            _ = services.AddTransient<NoteListViewModel>();

            _ = services.AddTransient<PersonBirthdayViewModel>();
            _ = services.AddTransient<PersonDetailViewModel>();
            _ = services.AddTransient<PersonListViewModel>();
            _ = services.AddTransient<PersonNameDetailViewModel>();
            _ = services.AddTransient<PlaceDetailViewModel>();
            _ = services.AddTransient<PlaceListViewModel>();

            _ = services.AddTransient<RepositoryDetailViewModel>();
            _ = services.AddTransient<RepositoryRefDetailViewModel>();
            _ = services.AddTransient<RepositoryListViewModel>();

            _ = services.AddTransient<SearchPageViewModel>();
            _ = services.AddTransient<SettingsViewModel>();
            _ = services.AddTransient<SourceDetailViewModel>();
            _ = services.AddTransient<SourceListViewModel>();

            _ = services.AddTransient<TagDetailViewModel>();
            _ = services.AddTransient<TagListViewModel>();

            //_ = services.AddTransient<SharedSharp.ViewModels.WhatsNewViewModel>();

            _ = services.AddTransient<NavigationPage>();

            // Essentials Interfaces
            _ = services.AddSingleton<Xamarin.Essentials.Interfaces.IDeviceInfo, Xamarin.Essentials.Implementation.DeviceInfoImplementation>();
            _ = services.AddSingleton<Xamarin.Essentials.Interfaces.IFileSystem, Xamarin.Essentials.Implementation.FileSystemImplementation>();
            _ = services.AddSingleton<Xamarin.Essentials.Interfaces.IPreferences, Xamarin.Essentials.Implementation.PreferencesImplementation>();

            ShardSharpCore.InitServicesAdd(ref services);

            return services.BuildServiceProvider();
        }

        private static async Task StartAtDetailPage()
        {
            if (PersonStartPage != null)
            {
                await PersonStartPage.UCNavigate();
            }

            if (FamilyStartModel != null)
            {
                await FamilyStartModel.UCNavigate();
            }
        }
    }
}