namespace GrampsView
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Services;
    using GrampsView.Views;

    using Prism.Events;
    using Prism.Services.Dialogs;

    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    public partial class AppShell : Xamarin.Forms.Shell
    {
        private readonly IDatabaseReloadDisplayService _DatabaseReloadDisplayService = new DatabaseReloadDisplayService();
        private readonly IFirstRunDisplayService _FirstRunDisplayService = new FirstRunDisplayService();
        private readonly IWhatsNewDisplayService _WhatsNewDisplayService = new WhatsNewDisplayService();

        private IDialogService _dialogService;
        private IEventAggregator _iocEventAggregator;
        private string CurrentPage = string.Empty;

        public AppShell(IEventAggregator iocEventAggregator, FirstRunDisplayService iocFirstRunDisplayService, WhatsNewDisplayService iocWhatsNewDisplayService, DatabaseReloadDisplayService iocDatabaseReloadDisplayService, IDialogService dialogService)

        {
            InitializeComponent();

            _iocEventAggregator = iocEventAggregator;
            _dialogService = dialogService;

            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            RegisterListRoutes();

            RegisterDetailRoutes();

            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            //_iocEventAggregator.GetEvent<AppStartFirstRunEvent>().Subscribe(ServiceFirstRun, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<AppStartLoadDataEvent>().Subscribe(ServiceLoadData, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<AppStartReloadDatabaseEvent>().Subscribe(ServiceReloadDatabase, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<AppStartWhatsNewEvent>().Subscribe(ServiceWhatsNew, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(LoadHubPage, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<DialogBoxEvent>().Subscribe(ErrorActionDialog, ThreadOption.UIThread);

            //BaseEventAggregator.GetEvent<PageNavigateEvent>().Subscribe(OnNavigateCommandExecuted, ThreadOption.UIThread);

            //BaseEventAggregator.GetEvent<PageNavigateParmsEvent>().Subscribe(OnNavigateParmsCommandExecuted, ThreadOption.UIThread);

            if (DataStore.Instance.DS.IsDataLoaded)
            {
                Shell.Current.GoToAsync("HubPage");
                return;
            }

            //ServiceFirstRun();
        }

        public void ErrorActionDialog(ActionDialogArgs argADA)
        {
            Contract.Assert(argADA != null);

            DialogParameters t = new DialogParameters
            {
                { "adaArgs", argADA },
            };

            //Using the dialog service as-is
            // TODO      BaseDialogService.ShowDialog("ErrorDialog", t);
        }

        public void ServiceLoadData()
        {
            if (CommonLocalSettings.DataSerialised)
            {
                // Start data load
                Shell.Current.GoToAsync(nameof(MessageLogPage));
                //_iocEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(MessageLogPage));
                _iocEventAggregator.GetEvent<DataLoadStartEvent>().Publish(false);
                return;
            }

            // No Serialised Data and made it this far so some problem has occurred. Load everything
            // from the beginning.
            Shell.Current.GoToAsync(nameof(FileInputHandlerPage));
            //_iocEventAggregator.GetEvent<PageNavigateEvent>().Publish(nameof(FileInputHandlerPage));
        }

        public void ServiceReloadDatabase()
        {
            if (!_DatabaseReloadDisplayService.ShowIfAppropriate(_iocEventAggregator))
            {
                _iocEventAggregator.GetEvent<AppStartLoadDataEvent>().Publish();
            }
        }

        public void ServiceWhatsNew()
        {
            if (!_WhatsNewDisplayService.ShowIfAppropriate(_iocEventAggregator))
            {
                _iocEventAggregator.GetEvent<AppStartReloadDatabaseEvent>().Publish();
            }
        }

        private void LoadHubPage(object obj)
        {
            Shell.Current.GoToAsync("HubPage");
        }

        //private async void OnMenuItemClicked(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync("HubPage");
        //}

        private void RegisterDetailRoutes()
        {
            Routing.RegisterRoute(nameof(HubPage), typeof(HubPage));

            Routing.RegisterRoute(nameof(PeopleGraphPage), typeof(PeopleGraphPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(FileInputHandlerPage), typeof(FileInputHandlerPage));

            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));

            Routing.RegisterRoute(nameof(CitationDetailPage), typeof(CitationDetailPage));
            Routing.RegisterRoute(nameof(EventDetailPage), typeof(EventDetailPage));
            Routing.RegisterRoute(nameof(FamilyDetailPage), typeof(FamilyDetailPage));
            Routing.RegisterRoute(nameof(MediaDetailPage), typeof(MediaDetailPage));

            Routing.RegisterRoute(nameof(AddressDetailPage), typeof(AddressDetailPage));
            Routing.RegisterRoute(nameof(PersonNameDetailPage), typeof(PersonNameDetailPage));
            Routing.RegisterRoute(nameof(MessageLogPage), typeof(MessageLogPage));
            Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));

            Routing.RegisterRoute(nameof(TagDetailPage), typeof(TagDetailPage));

            Routing.RegisterRoute(nameof(FileInputHandlerPage), typeof(FileInputHandlerPage));
            Routing.RegisterRoute(nameof(FirstRunPage), typeof(FirstRunPage));
            Routing.RegisterRoute(nameof(NeedDatabaseReloadPage), typeof(NeedDatabaseReloadPage));
            Routing.RegisterRoute(nameof(WhatsNewPage), typeof(WhatsNewPage));

            Routing.RegisterRoute(nameof(PersonDetailPage), typeof(PersonDetailPage));
            Routing.RegisterRoute(nameof(PlaceDetailPage), typeof(PlaceDetailPage));
            Routing.RegisterRoute(nameof(RepositoryDetailPage), typeof(RepositoryDetailPage));
            Routing.RegisterRoute(nameof(SourceDetailPage), typeof(SourceDetailPage));
        }

        private void RegisterListRoutes()
        {
            Routing.RegisterRoute(nameof(PersonBirthdayPage), typeof(PersonBirthdayPage));
            Routing.RegisterRoute(nameof(BookMarkListPage), typeof(BookMarkListPage));
            Routing.RegisterRoute(nameof(CitationListPage), typeof(CitationListPage));
            Routing.RegisterRoute(nameof(EventListPage), typeof(EventListPage));

            Routing.RegisterRoute(nameof(FamilyListPage), typeof(FamilyListPage));
            Routing.RegisterRoute(nameof(MediaListPage), typeof(MediaListPage));
            Routing.RegisterRoute(nameof(NoteListPage), typeof(NoteListPage));
            Routing.RegisterRoute(nameof(PersonListPage), typeof(PersonListPage));

            Routing.RegisterRoute(nameof(PlaceListPage), typeof(PlaceListPage));
            Routing.RegisterRoute(nameof(RepositoryListPage), typeof(RepositoryListPage));
            Routing.RegisterRoute(nameof(SourceListPage), typeof(SourceListPage));
            Routing.RegisterRoute(nameof(TagListPage), typeof(TagListPage));
        }

        //private async void OnNavigateCommandExecuted(string page)
        //{
        //    try
        //    {
        //        if (page != CurrentPage)
        //        {
        //            CurrentPage = page;

        // INavigationResult result = await
        // BaseNavigationService.NavigateAsync(nameof(NavigationPage) + "/" + page.Trim()).ConfigureAwait(false);

        //            if (!result.Success)
        //            {
        //                DataStore.Instance.CN.NotifyException("OnNavigateCommandExecuted", result.Exception);
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        DataStore.Instance.CN.NotifyException("OnNavigateCommandExecuted", ex);
        //        throw;
        //    }
        //}

        //private async void OnNavigateParmsCommandExecuted(INavigationParameters obj)
        //{
        //    obj.TryGetValue(CommonConstants.NavigationParameterTargetView, out string target);

        // //if (target != CurrentPage) //{ CurrentPage = target;

        // string t = nameof(NavigationPage) + "/" + target.Trim();

        // INavigationResult result = await BaseNavigationService.NavigateAsync(t, obj).ConfigureAwait(false);

        //    if (!result.Success)
        //    {
        //        DataStore.Instance.CN.NotifyException("OnNavigateParmsCommandExecuted", result.Exception);
        //    }
        //    //}
        //}
    }
}