namespace GrampsView.Droid
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;

    using FFImageLoading.Forms.Platform;

    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;
    using GrampsView.Droid.Common;

    using Prism;
    using Prism.Ioc;

    using System;
    using System.Diagnostics.Contracts;
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

    public class CustomLogger : FFImageLoading.Helpers.IMiniLogger

    {
        public void Debug(string message)

        {
            Console.WriteLine(message);
        }

        public void Error(string errorMessage)

        {
            Console.WriteLine(errorMessage);
        }

        public void Error(string errorMessage, Exception ex)
        {
            Contract.Assert(ex != null);

            Error(errorMessage + System.Environment.NewLine + ex.ToString());
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
                "MediaElement_Experimental",
                "RadioButton_Experimental",
                "Shell_UWP_Experimental"
                });

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // App Center Distribute Distribute.SetEnabledForDebuggableBuild(true);

            // FFImageLoading Init
            CachedImageRenderer.Init(enableFastRenderer: false);

            CachedImageRenderer.InitImageViewHandler();

            Platform.Init(this, savedInstanceState);

            // Load the app
            LoadApplication(new App(new AndroidInitializer()));
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