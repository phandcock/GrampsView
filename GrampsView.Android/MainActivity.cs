namespace GrampsView.Droid
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;

    using FFImageLoading.Forms.Platform;

    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;
    using GrampsView.Droid.Common;

    using GrampsView;

    using Prism;
    using Prism.Ioc;

    using System;
    using System.Threading.Tasks;

    using Xamarin.Essentials;

    public class AndroidInitializer : IPlatformInitializer
    {
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
                Theme = "@style/MainTheme"
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

            // Init things
            Xamarin.Forms.Forms.SetFlags(new string[] {
                "AppTheme_Experimental",
                "DragAndDrop_Experimental",
                "RadioButton_Experimental"
                });

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // App Center Distribute Distribute.SetEnabledForDebuggableBuild(true);

            // FFImageLoading Init
            CachedImageRenderer.Init(enableFastRenderer: false);

            CachedImageRenderer.InitImageViewHandler();

            Platform.Init(this, savedInstanceState);

            // Set Status Bar background to Light or Dark as required
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            // Load the app
            LoadApplication(new App());
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new Exception(nameof(CurrentDomainOnUnhandledException), unhandledExceptionEventArgs.ExceptionObject as Exception);
            DataStore.Instance.CN.NotifyException("CurrentDomainOnUnhandledException", newExc);
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception(nameof(CurrentDomainOnUnhandledException), unobservedTaskExceptionEventArgs.Exception);
            DataStore.Instance.CN.NotifyException("TaskSchedulerOnUnobservedTaskException", newExc);
        }
    }
}