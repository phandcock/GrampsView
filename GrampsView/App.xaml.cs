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


        private void StartUp()
        {


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
