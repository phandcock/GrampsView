// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Repository;

using SharedSharp.Common.Interfaces;
using SharedSharp.Sizes;

namespace GrampsView
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            StartUp();

            _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new SharedSharp.Events.SSharpAppStartEvent(true));
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Created += (s, e) =>
            {
                // This goes here so AppInit with WhatsNew and FirstRun work
                _ = Ioc.Default.GetRequiredService<ISharedSharpAppInit>().Init();

                Ioc.Default.GetRequiredService<ISharedSizes>().HandleWindowSizeChanged((s as Window).Width, (s as Window).Height);
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
        }
    }
}