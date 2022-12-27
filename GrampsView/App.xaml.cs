using GrampsView.Data.Repository;

using SharedSharp.Common.Interfaces;

namespace GrampsView
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            StartUp();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Created += (s, e) =>
            {
                // Custom logic
                _ = Ioc.Default.GetRequiredService<ISharedSharpAppInit>().Init();

                Ioc.Default.GetRequiredService<ISharedSharpSizes>().HandleWindowSizeChanged((s as Microsoft.Maui.Controls.Window).Width, (s as Microsoft.Maui.Controls.Window).Height);

            };

            return window;
        }

        private void StartUp()
        {
            // Setup various support frameworks
            VersionTracking.Track();

            _ = Ioc.Default.GetRequiredService<IDataRepositoryManager>();

            // App Setup
            Current.UserAppTheme = SharedSharpSettings.ApplicationTheme;

            SharedSharpSettings.DatabaseVersionMin = Common.Constants.GrampsViewDatabaseVersion;

            // Get Going
            //    StartAtDetailPage().GetAwaiter().GetResult();

            if (DataStore.Instance.DS.IsDataLoaded)
            {
                SharedSharpNavigation.NavigateHub();

                return;
            }

            //// Get Going
            //_ = Ioc.Default.GetRequiredService<ISharedSharpAppInit>().Init().ConfigureAwait(false);
        }
    }
}