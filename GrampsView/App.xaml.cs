// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Repository;
using GrampsView.Views;

using SharedSharp.Common.Interfaces;

namespace GrampsView
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BaseNavigation();

            StartUp();
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

            // Custom logic
            _ = Ioc.Default.GetRequiredService<ISharedSharpAppInit>().Init();

            //if (DataStore.Instance.DS.IsDataLoaded)
            //{
            //    Navigation.PushAsync(new HubPage());

            //    return;
            //}

            //// Get Going
            //_ = Ioc.Default.GetRequiredService<ISharedSharpAppInit>().Init().ConfigureAwait(false);
        }
    }
}