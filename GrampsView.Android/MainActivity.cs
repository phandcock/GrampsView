namespace GrampsView.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;

    using FFImageLoading.Forms.Platform;

    using GrampsView;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;
    using GrampsView.Droid.Common;

    using Prism;
    using Prism.Ioc;

    using System;
    using System.Threading.Tasks;

    using Xamarin.Essentials;

    public class AndroidInitializer : IPlatformInitializer
    {
        public AndroidInitializer()
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterSingleton<IPlatformSpecific, PlatformSpecific>();
        }
    }

    /// <summary>
    /// Main Activity class
    /// </summary>
    [Activity(
                MainLauncher = false,
                Theme = "@style/MainTheme",
                ConfigurationChanges = ConfigChanges.ScreenSize |
                                        ConfigChanges.UiMode |
                                        ConfigChanges.Orientation |
                                        ConfigChanges.ScreenLayout |
                                        ConfigChanges.SmallestScreenSize,
                ScreenOrientation = ScreenOrientation.FullSensor
             )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            // FFImageLoading Init
            CachedImageRenderer.Init(enableFastRenderer: false);

            CachedImageRenderer.InitImageViewHandler();

            Platform.Init(this, savedInstanceState);

            ////// Init things
            //Xamarin.Forms.Forms.SetFlags(new string[] {
            //    "DragAndDrop_Experimental"
            //    });

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // App Center Distribute Distribute.SetEnabledForDebuggableBuild(true);

            // Set Status Bar background to Light or Dark as required
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            // Load the app
            LoadApplication(new App(new AndroidInitializer()));
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            Exception newExc = new Exception(nameof(CurrentDomainOnUnhandledException), unhandledExceptionEventArgs.ExceptionObject as Exception);
            DataStore.Instance.CN.NotifyException("CurrentDomainOnUnhandledException", newExc);
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            Exception newExc = new Exception(nameof(CurrentDomainOnUnhandledException), unobservedTaskExceptionEventArgs.Exception);
            DataStore.Instance.CN.NotifyException("TaskSchedulerOnUnobservedTaskException", newExc);
        }
    }
}