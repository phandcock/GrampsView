using FFImageLoading.Forms.Platform;

using Foundation;

using GrampsView.Data.Repository;

using Microsoft.AppCenter.Distribute;

using Prism;
using Prism.Ioc;

using System;
using System.Threading.Tasks;

using UIKit;

namespace GrampsView.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Xamarin.Forms.Device.SetFlags(new string[] {
            //    });

            global::Xamarin.Forms.Forms.Init();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            Distribute.DontCheckForUpdatesInDebug();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            CachedImageRenderer.InitImageSourceHandler();

            //GrampsView.UserControls.iOS.Renderers.BorderlessEntryRenderer.Init();

            LoadApplication(new App(new IOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
            DataStore.Instance.CN.NotifyException("CurrentDomainOnUnhandledException", newExc);
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
            DataStore.Instance.CN.NotifyException("TaskSchedulerOnUnobservedTaskException", newExc);
        }
    }

    public class IOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}