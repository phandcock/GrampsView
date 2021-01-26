namespace GrampsView.UWP
{
    using FFImageLoading.Forms;

    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using Microsoft.AppCenter;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading.Tasks;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>

        public App()
        {
            this.InitializeComponent();

            this.Suspending += OnSuspending;

            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            Current.UnhandledException += new Windows.UI.Xaml.UnhandledExceptionEventHandler(UnhandledExceptionHandler);
        }

        protected override void OnActivated(IActivatedEventArgs e)
        {
            // Handle Protocol Activation
            //
            // Handles URIs of the format used by Gramps internally gramps://person/handle/12345
            if (e.Kind == ActivationKind.Protocol)
            {
                ProtocolActivatedEventArgs uriArgs = e as ProtocolActivatedEventArgs;

                if (uriArgs != null)
                {
                    string[] uriSegments = uriArgs.Uri.Segments;

                    if (uriSegments[1] != "handle/")
                    {
                        // TODO Handle error
                    }

                    switch (uriArgs.Uri.Host)
                    {
                        case CommonConstants.ModelNameFamily:
                            {
                                HLinkFamilyModel targetFamily = new HLinkFamilyModel
                                {
                                    HLinkKey = uriSegments[2]
                                };

                                targetFamily.UCNavigate();

                                break;
                            }

                        case CommonConstants.ModelNamePerson:
                            {
                                HLinkPersonModel targetPerson = new HLinkPersonModel
                                {
                                    HLinkKey = uriSegments[2]
                                };

                                targetPerson.UCNavigate();

                                break;
                            }

                        default:
                            {
                                // TODO Handle bad arg better

                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user. Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">
        /// Details about the launch request and process.
        /// </param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do not repeat app initialization when the Window already has content, just ensure
            // that the window is active
            if (!(Window.Current.Content is Windows.UI.Xaml.Controls.Frame rootFrame))
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Windows.UI.Xaml.Controls.Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                // FFImageLoading
                ///////////////////////////////////////////////////////////////////////////////////////////
                FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

                var config = new FFImageLoading.Config.Configuration()

                {
                    VerboseLogging = true,

                    VerbosePerformanceLogging = false,

                    VerboseMemoryCacheLogging = false,

                    VerboseLoadingCancelledLogging = false,
                };

                FFImageLoading.ImageService.Instance.Initialize(config);

                var assembliesToInclude = new List<Assembly>
                    {
                        typeof(CachedImage).GetTypeInfo().Assembly,
                        typeof(FFImageLoading.Forms.Platform.CachedImageRenderer).GetTypeInfo().Assembly,
                        typeof(GrampsView.App).GetTypeInfo().Assembly
                    };

                // App Center
                ////////////////////////////////////////////////////////////////////////////////////////////////Debug.WriteLine(initString, "AppCenterInit");
                Debug.WriteLine(AppCenter.SdkVersion, "UWP SDK");

                // Forms Init
                ////////////////////////////////////////////////////////////////////////////////////////////////
                Xamarin.Forms.Forms.SetFlags(new string[] {
                    "AppTheme_Experimental",
                    "DragAndDrop_Experimental",
                    "RadioButton_Experimental",
                    "Shell_UWP_Experimental"
                });

                Xamarin.Forms.Forms.Init(e, assembliesToInclude);

                //GrampsView.UserControls.UWP.Renderers.BorderlessEntryRenderer.Init();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page, configuring
                // the new page by passing required information as a navigation parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception(nameof(TaskSchedulerOnUnobservedTaskException), unobservedTaskExceptionEventArgs.Exception);

            DataStore.Instance.CN.NotifyException("TaskSchedulerOnUnobservedTaskException", newExc);
        }

        private static void UnhandledExceptionHandler(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs args)
        {
            Exception e = args.Exception;

            DataStore.Instance.CN.NotifyException(string.Format("UnhandledExceptionHandler-{0}", args.Message), e);
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">
        /// The Frame which failed navigation
        /// </param>
        /// <param name="e">
        /// Details about the navigation failure
        /// </param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            DataStore.Instance.CN.NotifyError("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">
        /// The source of the suspend request.
        /// </param>
        /// <param name="e">
        /// Details about the suspend request.
        /// </param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}