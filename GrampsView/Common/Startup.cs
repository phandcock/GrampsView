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

        private async void ServiceLoadData()
        {
            if (CommonLocalSettings.DataSerialised)
            {
                // Start data load

                // await CommonRoutines.NavigateAsync(nameof(MessageLogPage));

                _iocEventAggregator.GetEvent<DataLoadStartEvent>().Publish();
                return;
            }

            // No Serialised Data and made it this far so some problem has occurred. Load everything
            // from the beginning.
            var t = Shell.Current.Navigation.NavigationStack;

            await CommonRoutines.NavigateAsync(nameof(FileInputHandlerPage));
        }

        private void ServiceReloadDatabase()
        {
            if (!_DatabaseReloadDisplayService.ShowIfAppropriate(_iocEventAggregator))
            {
                _iocEventAggregator.GetEvent<AppStartLoadDataEvent>().Publish();
            }
        }

        private void ServiceWhatsNew()
        {
            if (!_WhatsNewDisplayService.ShowIfAppropriate(_iocEventAggregator))
            {
                _iocEventAggregator.GetEvent<AppStartReloadDatabaseEvent>().Publish();
            }
        }

        private void StartEvents(IEventAggregator iocEventAggregator, FirstRunDisplayService iocFirstRunDisplayService, WhatsNewDisplayService iocWhatsNewDisplayService, DatabaseReloadDisplayService iocDatabaseReloadDisplayService, IDialogService dialogService)

        {
            _iocEventAggregator = iocEventAggregator;
            _dialogService = dialogService;

            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            _iocEventAggregator.GetEvent<AppStartLoadDataEvent>().Subscribe(ServiceLoadData, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<AppStartReloadDatabaseEvent>().Subscribe(ServiceReloadDatabase, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<AppStartWhatsNewEvent>().Subscribe(ServiceWhatsNew, ThreadOption.UIThread);

            _iocEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(CommonRoutines.NavigateHub, ThreadOption.UIThread);

            StartProcessing();
        }

        private void StartProcessing()
        {
            MainThread.BeginInvokeOnMainThread(() =>
           {
                // TODO check if fix for bug https://github.com/xamarin/Xamarin.Forms/issues/6681

                if (DataStore.Instance.DS.IsDataLoaded)
               {
                   CommonRoutines.NavigateHub();
                   return;
               }

               if (!_FirstRunDisplayService.ShowIfAppropriate())
               {
                   _iocEventAggregator.GetEvent<AppStartWhatsNewEvent>().Publish();
               }
           });
        }
    }
}