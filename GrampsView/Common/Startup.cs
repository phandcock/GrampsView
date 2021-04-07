namespace GrampsView
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Services;
    using GrampsView.Views;

    using Prism.Events;

    using System.Threading.Tasks;

    public sealed partial class App
    {
        private IDatabaseReloadDisplayService _DatabaseReloadDisplayService = new DatabaseReloadDisplayService();

        private IFirstRunDisplayService _FirstRunDisplayService = new FirstRunDisplayService();

        private IEventAggregator _iocEventAggregator;
        private IWhatsNewDisplayService _WhatsNewDisplayService = new WhatsNewDisplayService();

        private async void ServiceLoadData()
        {
            //var t = Shell.Current.Navigation.NavigationStack;

            if (CommonLocalSettings.DataSerialised)
            {
                // Start data load

                // await CommonRoutines.NavigateAsync(nameof(MessageLogPage));

                _iocEventAggregator.GetEvent<DataLoadStartEvent>().Publish();
                return;
            }

            // No Serialised Data and made it this far so some problem has occurred. Load everything
            // from the beginning.
            await CommonRoutines.NavigateAsync(nameof(FileInputHandlerPage));

            //await CommonRoutines.NavigateAsync(nameof(HubPage) + "///" + nameof(FileInputHandlerPage));
        }

        private void StartEvents(IEventAggregator iocEventAggregator, FirstRunDisplayService iocFirstRunDisplayService, WhatsNewDisplayService iocWhatsNewDisplayService, DatabaseReloadDisplayService iocDatabaseReloadDisplayService)

        {
            _iocEventAggregator = iocEventAggregator;
            //_dialogService = dialogService;

            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;

            //_iocEventAggregator.GetEvent<AppStartLoadDataEvent>().Subscribe(ServiceLoadData, ThreadOption.UIThread);

            StartProcessing();
        }

        private async Task StartProcessing()
        {
            if (DataStore.Instance.DS.IsDataLoaded)
            {
                // CommonRoutines.NavigateHub();
                return;
            }

            if (await _FirstRunDisplayService.ShowIfAppropriate())
            {
                await CommonRoutines.NavigateAsync(nameof(FirstRunPage));
                return;
            }

            if (await _WhatsNewDisplayService.ShowIfAppropriate())
            {
                await CommonRoutines.NavigateAsync(nameof(WhatsNewPage));
                return;
            }

            if (await _DatabaseReloadDisplayService.ShowIfAppropriate())
            {
                await CommonRoutines.NavigateAsync(nameof(NeedDatabaseReloadPage));

                return;
            }

            ServiceLoadData();
        }
    }
}