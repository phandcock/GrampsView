// Copyright (c) phandcock.  All rights reserved.

using Android.App;
using Android.Content.PM;
using Android.OS;

using Microsoft.AppCenter.Distribute;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

namespace GrampsView.Platforms.Android
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            // Enable Distribute for Debug builds
            Distribute.SetEnabledForDebuggableBuild(true);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            Exception newExc = new(nameof(CurrentDomainOnUnhandledException), unhandledExceptionEventArgs.ExceptionObject as Exception);

            Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(newExc,
                    new ErrorInfo("CurrentDomainOnUnhandledException")
                    {
                        new CardListLine("Sender",sender.GetType().ToString()),
                    });
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            Exception newExc = new(nameof(CurrentDomainOnUnhandledException), unobservedTaskExceptionEventArgs.Exception);

            Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(newExc,
                  new ErrorInfo("TaskSchedulerOnUnobservedTaskException")
                    {
                        new CardListLine("Sender",sender.GetType().ToString()),
                    });
        }
    }
}