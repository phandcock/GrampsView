namespace GrampsView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data;
    using GrampsView.Data.External.StoreSerial;
    using GrampsView.Data.ExternalStorage;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
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

    using System.Diagnostics;
    using System.Globalization;
    using System.Threading.Tasks;

    using Unity;

    using Xamarin.Essentials;
    using Xamarin.Forms;

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

            // This lookup NOT required for Windows platforms - the Culture will be automatically set
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                // determine the correct, supported .NET culture
                DependencyService.Get<ILocalize>();

                CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

                // Assets.Strings.AppResources.Culture = ci; TODO set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            AppCenterInit();

            Container.Resolve<IPlatformSpecific>();

            // Resove it here. TODO Have each class resolve its own copy using the service locator
            // pattern from prism.
            DataStore.Instance.CN = Container.Resolve<ICommonNotifications>();

            Container.Resolve<IDataRepositoryManager>();


            Container.Resolve<IStartAppLoad>();

            //// Subscribe to changes of screen metrics
            DeviceDisplay.MainDisplayInfoChanged += async (s, a) =>
            {
                await OnMainDisplayInfoChanged(s, a);
            };
            DataStore.Instance.AD.ScreenSizeInit();

            VersionTracking.Track();

            Application.Current.UserAppTheme = CommonLocalSettings.ApplicationTheme;

            MainPage = new AppShell();

            Shell.Current.GoToAsync("///HubPage").GetAwaiter().GetResult();

            StartAtDetailPage().GetAwaiter().GetResult();
        }

        protected override void OnResume()
        {
            Debug.WriteLine("Resuming");

            base.OnResume();

            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("Sleeping");

            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            if (DataStore.Instance.DS.IsDataLoaded)
            {
                CommonRoutines.NavigateHub();

                return;
            }

            // CardSizes.Current.ReCalculateCardWidths((DeviceDisplay.MainDisplayInfo.Width /
            // DeviceDisplay.MainDisplayInfo.Density), (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density));

            // TODO create platform specific check for allowed rotations until xamarin.essentials
            // gives me the data

            Container.Resolve<IStartAppLoad>().StartProcessing();
        }

        protected override void RegisterTypes(IContainerRegistry container)
        {
            container.RegisterForNavigation<AboutPage, AboutViewModel>();
            container.RegisterForNavigation<AddressDetailPage, AddressDetailViewModel>();
            container.RegisterForNavigation<AttributeDetailPage, AttributeDetailViewModel>();

            container.RegisterForNavigation<BookMarkListPage, BookMarkListViewModel>();

            container.RegisterForNavigation<ChildRefDetailPage, ChildRefDetailViewModel>();
            container.RegisterForNavigation<CitationDetailPage, CitationDetailViewModel>();
            container.RegisterForNavigation<CitationListPage, CitationListViewModel>();

            container.RegisterForNavigation<DateDetailPage, DateDetailViewModel>();

            container.RegisterForNavigation<EventDetailPage, EventDetailViewModel>();
            container.RegisterForNavigation<EventListPage, EventListViewModel>();

            container.RegisterForNavigation<FamilyDetailPage, FamilyDetailViewModel>();
            container.RegisterForNavigation<FamilyListPage, FamilyListViewModel>();
            container.RegisterForNavigation<FileInputHandlerPage, FileInputHandlerViewModel>();
            container.RegisterForNavigation<FirstRunPage, FirstRunViewModel>();

            container.RegisterForNavigation<HubPage, HubViewModel>();

            container.RegisterForNavigation<MessageLogPage, MessageLogViewModel>();
            container.RegisterForNavigation<MediaDetailPage, MediaDetailViewModel>();
            container.RegisterForNavigation<MediaListPage, MediaListViewModel>();

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
            container.RegisterForNavigation<RepositoryRefDetailPage, RepositoryRefDetailViewModel>();
            container.RegisterForNavigation<RepositoryListPage, RepositoryListViewModel>();

            container.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            container.RegisterForNavigation<SettingsPage, SettingsViewModel>();
            container.RegisterForNavigation<SourceDetailPage, SourceDetailViewModel>();
            container.RegisterForNavigation<SourceListPage, SourceListViewModel>();

            container.RegisterForNavigation<TagDetailPage, TagDetailViewModel>();
            container.RegisterForNavigation<TagListPage, TagListViewModel>();

            container.RegisterForNavigation<NavigationPage>();

            container.RegisterSingleton<IStartAppLoad, StartAppLoad>();
            container.RegisterSingleton<ICommonLogging, CommonLogging>();
            container.RegisterSingleton<ICommonNotifications, CommonNotifications>();
            container.RegisterSingleton<IDataRepositoryManager, DataRepositoryManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
            container.RegisterSingleton<IStorePostLoad, StorePostLoad>();
            container.RegisterSingleton<IGrampsStoreSerial, GrampsStoreSerial>();
            container.RegisterSingleton<IStoreXML, StoreXML>();
            container.RegisterSingleton<IStoreFile, StoreFile>();

            container.RegisterForNavigation<WhatsNewPage, WhatsNewViewModel>();
            container.RegisterForNavigation<FirstRunPage>();
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

        //This code currently runs one rotation behind and doe snto set the window size properly on UWP.
        //See CardSizxes for the current hack fix.

        private async Task OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            DataStore.Instance.AD.ScreenSizeInit();

            // // Process changes // EventAggregator ea = this.Container.Resolve<EventAggregator>();

            // //if (!(ea is null)) // { //var t = DeviceDisplay.MainDisplayInfo; // //
            // ea.GetEvent<OrientationChanged>().Publish(e.DisplayInfo.Orientation); because seems
            // // to be one rotation behind on emulator. Try the old school way until fixed if
            // (e.DisplayInfo.Width > e.DisplayInfo.Height) {
            // DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Landscape; } else { //
            // DataStore.Instance.AD.CurrentOrientation = DisplayOrientation.Portrait; }

            // // // Card width reset CardSizes.Current.ReCalculateCardWidths();
            ////     }
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