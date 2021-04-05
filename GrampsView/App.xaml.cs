using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data;
using GrampsView.Data.External.StoreSerial;
using GrampsView.Data.ExternalStorage;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.Services;
using GrampsView.ViewModels;
using GrampsView.Views;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

using Prism;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;

using System.Diagnostics;
using System.Threading.Tasks;

using Unity;

using Xamarin.Essentials;
using Xamarin.Forms;

// Needs to be here to fix UWP font issue - TODO https://github.com/xamarin/Xamarin.Forms/issues/12404
[assembly: ExportFont("fa-solid-900.ttf", Alias = "FA-Solid")]

namespace GrampsView
{
    public sealed partial class App
    {
        private static HLinkFamilyModel FamilyStartModel = null;

        private static HLinkPersonModel PersonStartPage = null;

        public App()
        {
        }

        public App(IPlatformInitializer initializer)
                            : base(initializer)
        {
        }

        public App(IPlatformInitializer initializer, bool setFormsDependencyResolver)
                            : base(initializer, setFormsDependencyResolver)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
        }

        protected override void OnAppLinkRequestReceived(System.Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            //Debug.WriteLine("====== resource debug info =========");

            //var assembly = typeof(App).GetTypeInfo().Assembly;

            //foreach (var res in assembly.GetManifestResourceNames())
            //{
            //    Debug.WriteLine("found resource: " + res);
            //}

            //Debug.WriteLine("====================================");

            // This lookup NOT required for Windows platforms - the Culture will be automatically set
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                // determine the correct, supported .NET culture
                DependencyService.Get<ILocalize>();

                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

                // Assets.Strings.AppResources.Culture = ci; TODO set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            // Subscribe to changes of screen metrics
            DeviceDisplay.MainDisplayInfoChanged += async (s, a) => { await OnMainDisplayInfoChanged(s, a); };

            VersionTracking.Track();

            Application.Current.UserAppTheme = CommonLocalSettings.ApplicationTheme;

            MainPage = new AppShell();

            StartAtDetailPage();

            Shell.Current.GoToAsync("///HubPage");
        }

        protected override void OnResume()
        {
            // Support IApplicationLifecycleAware

            if (DataStore.Instance.DS.IsDataLoaded)
            {
                //Shell.Current.Navigation.PopToRootAsync(animated: true);
                //return;
            }

            base.OnResume();

            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Support IApplicationLifecycleAware

            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            if (DataStore.Instance.DS.IsDataLoaded)
            {
                Common.CommonRoutines.NavigateHub();
                //Shell.Current.Navigation.PopToRootAsync(animated: true);
                return;
            }

            AppCenterInit();

            CardSizes.Current.ReCalculateCardWidths();

            // TODO create platform specific check for allowed rotations until xamarin.essentials
            // gives me the data

            Container.Resolve<IPlatformSpecific>();

            DataStore.Instance.CN = Container.Resolve<ICommonNotifications>();

            Container.Resolve<IDataRepositoryManager>();

            StartEvents(Container.Resolve<IEventAggregator>(), Container.Resolve<FirstRunDisplayService>(), Container.Resolve<WhatsNewDisplayService>(),
                Container.Resolve<DatabaseReloadDisplayService>(), Container.Resolve<IDialogService>());
        }

        protected override void RegisterTypes(IContainerRegistry container)
        {
            container.RegisterForNavigation<AboutPage, AboutViewModel>();
            container.RegisterForNavigation<AddressDetailPage, AddressDetailViewModel>();

            container.RegisterForNavigation<BookMarkListPage, BookMarkListViewModel>();

            container.RegisterForNavigation<CitationDetailPage, CitationDetailViewModel>();
            container.RegisterForNavigation<CitationListPage, CitationListViewModel>();

            container.RegisterForNavigation<EventDetailPage, EventDetailViewModel>();
            container.RegisterForNavigation<EventListPage, EventListViewModel>();

            container.RegisterForNavigation<FamilyDetailPage, FamilyDetailViewModel>();
            container.RegisterForNavigation<FamilyListPage, FamilyListViewModel>();
            container.RegisterForNavigation<FileInputHandlerPage, FileInputHandlerViewModel>();
            container.RegisterForNavigation<FirstRunPage, FirstRunViewModel>();

            container.RegisterForNavigation<HubPage, HubViewModel>();

            container.RegisterForNavigation<MediaDetailPage, MediaDetailViewModel>();
            container.RegisterForNavigation<MediaListPage, MediaListViewModel>();
            container.RegisterForNavigation<MessageLogPage, MessageLogViewModel>();

            container.RegisterForNavigation<NeedDatabaseReloadPage, NeedDatabaseReloadViewModel>();
            container.RegisterForNavigation<NoteDetailPage, NoteDetailViewModel>();
            container.RegisterForNavigation<NoteListPage, NoteListViewModel>();

            container.RegisterForNavigation<PeopleGraphPage, PeopleGraphViewModel>();
            container.RegisterForNavigation<PersonBirthdayPage, PersonBirthdayViewModel>();
            container.RegisterForNavigation<PersonDetailPage, PersonDetailViewModel>();
            container.RegisterForNavigation<PersonListPage, PersonListViewModel>();
            container.RegisterForNavigation<PersonNameDetailPage, PersonNameDetailViewModel>();
            container.RegisterForNavigation<PlaceDetailPage, PlaceDetailViewModel>();
            container.RegisterForNavigation<PlaceListPage, PlaceListViewModel>();

            container.RegisterForNavigation<RepositoryDetailPage, RepositoryDetailViewModel>();
            container.RegisterForNavigation<RepositoryListPage, RepositoryListViewModel>();

            container.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            container.RegisterForNavigation<SettingsPage, SettingsViewModel>();
            container.RegisterForNavigation<SourceDetailPage, SourceDetailViewModel>();
            container.RegisterForNavigation<SourceListPage, SourceListViewModel>();

            container.RegisterForNavigation<TagDetailPage, TagDetailViewModel>();
            container.RegisterForNavigation<TagListPage, TagListViewModel>();

            container.RegisterForNavigation<NavigationPage>();

            //container.RegisterDialog<ErrorDialog, ErrorDialogViewModel>();

            container.RegisterSingleton<ICommonLogging, CommonLogging>();
            container.RegisterSingleton<IDataLog, CommonDataLog>();
            container.RegisterSingleton<ICommonNotifications, CommonNotifications>();
            container.RegisterSingleton<IDataRepositoryManager, DataRepositoryManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
            container.RegisterSingleton<IStorePostLoad, StorePostLoad>();
            container.RegisterSingleton<IGrampsStoreSerial, GrampsStoreSerial>();
            container.RegisterSingleton<IStoreXML, StoreXML>();
            container.RegisterSingleton<IStoreFile, StoreFile>();

            container.RegisterSingleton<WhatsNewDisplayService>();
            container.RegisterForNavigation<WhatsNewPage, WhatsNewViewModel>();

            container.RegisterSingleton<FirstRunDisplayService>();
            container.RegisterForNavigation<FirstRunPage>();

            container.RegisterSingleton<DatabaseReloadDisplayService>();
            container.RegisterForNavigation<NeedDatabaseReloadPage>();
        }

        /// <summary>
        /// Initialize App Center.
        /// </summary>
        private static void AppCenterInit()
        {
            string initString = "uwp=" + Secret.UWPSecret + ";" +
                                "android=" + Secret.AndroidSecret + ";" +
                                "ios=" + Secret.IOSSecret;

            Debug.WriteLine(initString, "AppCenterInit");

            AppCenter.Start(initString,
                            typeof(Analytics), typeof(Crashes), typeof(Distribute));

            Distribute.SetEnabledAsync(true);

            Distribute.CheckForUpdate();
        }

        private async Task OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            // Process changes
            EventAggregator ea = this.Container.Resolve<EventAggregator>();

            if (!(ea is null))
            {
                //var t = DeviceDisplay.MainDisplayInfo;

                // TODO Is this needed? TODO Has hack in ViewBase to set orientation properly on
                // Android until I work out what is wrong
                //
                // ea.GetEvent<OrientationChanged>().Publish(e.DisplayInfo.Orientation);

                // TODO fu because seems to be one rotation behind on emulator. Try the old school
                // way until fixed if (e.DisplayInfo.Width > e.DisplayInfo.Height) {
                // DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Landscape; } else {
                // DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Portrait; }

                // // Card width reset CardSizes.Current.ReCalculateCardWidths();
            }
        }

        private async Task StartAtDetailPage()
        {
            if (PersonStartPage != null)
            {
                await PersonStartPage.UCNavigate();
            }

            if (FamilyStartModel != null)
            {
                await FamilyStartModel.UCNavigate();
            }
        }
    }
}