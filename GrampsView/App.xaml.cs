using GrampsView.Data.Repository;

using SharedSharp.Common.Interfaces;

namespace GrampsView
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            StartUp();
        }

        [Obsolete]
        private void StartUp()
        {
            //// This lookup NOT required for Windows platforms - the Culture will be automatically set
            //if (Device.RuntimePlatform is Device.iOS or Device.Android)
            //{
            //    // Determine the correct, supported .NET culture
            //    _ = DependencyService.Get<ILocalize>();

            //    CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

            //    // Assets.Strings.AppResources.Culture = ci; TODO set the RESX for resource localization
            //    DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            //}

            // Setup various support frameworks
            VersionTracking.Track();

            _ = Ioc.Default.GetService<IDataRepositoryManager>();

            // App Setup
            Application.Current.UserAppTheme = SharedSharpSettings.ApplicationTheme;

            SharedSharpSettings.DatabaseVersionMin = GrampsView.Common.Constants.GrampsViewDatabaseVersion;

            // Get Going
            //    StartAtDetailPage().GetAwaiter().GetResult();

            if (DataStore.Instance.DS.IsDataLoaded)
            {
                SharedSharpNavigation.NavigateHub();

                return;
            }

            // Get Going
            _ = Ioc.Default.GetService<ISharedSharpAppInit>().Init().ConfigureAwait(false);
        }
    }
}
