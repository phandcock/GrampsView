namespace GrampsView
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Services;
    using GrampsView.Views;

    using Prism.Events;
    using Prism.Services.Dialogs;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public sealed partial class App
    {
        private IDatabaseReloadDisplayService _DatabaseReloadDisplayService = new DatabaseReloadDisplayService();
        private IDialogService _dialogService;
        private IFirstRunDisplayService _FirstRunDisplayService = new FirstRunDisplayService();
        private IEventAggregator _iocEventAggregator;
        private IWhatsNewDisplayService _WhatsNewDisplayService = new WhatsNewDisplayService();
        private string CurrentPage = string.Empty;

        public async void ServiceLoadData()
        {
            if (CommonLocalSettings.DataSerialised)
            {
                // Start data load

                _iocEventAggregator.GetEvent<DataLoadStartEvent>().Publish(false);
                return;
            }

            // No Serialised Data and made it this far so some problem has occurred. Load everything
            // from the beginning.
            await Shell.Current.GoToAsync(nameof(FileInputHandlerPage));
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

        public void StartEvents(IEventAggregator iocEventAggregator, FirstRunDisplayService iocFirstRunDisplayService, WhatsNewDisplayService iocWhatsNewDisplayService, DatabaseReloadDisplayService iocDatabaseReloadDisplayService, IDialogService dialogService)

        {
            _iocEventAggregator = iocEventAggregator;
            _dialogService = dialogService;

            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            //_iocEventAggregator.GetEvent<AppStartFirstRunEvent>().Subscribe(ServiceFirstRun, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<AppStartLoadDataEvent>().Subscribe(ServiceLoadData, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<AppStartReloadDatabaseEvent>().Subscribe(ServiceReloadDatabase, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<AppStartWhatsNewEvent>().Subscribe(ServiceWhatsNew, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(LoadHubPage, ThreadOption.UIThread);

            //BaseEventAggregator.GetEvent<PageNavigateEvent>().Subscribe(OnNavigateCommandExecuted, ThreadOption.UIThread);

            //BaseEventAggregator.GetEvent<PageNavigateParmsEvent>().Subscribe(OnNavigateParmsCommandExecuted, ThreadOption.UIThread);

            // Always display the log
            Shell.Current.GoToAsync(nameof(MessageLogPage));

            if (DataStore.Instance.DS.IsDataLoaded)
            {
                LoadHubPage();
                return;
            }

            if (!_FirstRunDisplayService.ShowIfAppropriate())
            {
                _iocEventAggregator.GetEvent<AppStartWhatsNewEvent>().Publish();
            }
        }

        private async void LoadHubPage()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.GoToAsync("HubPage");
            });
        }
    }
}