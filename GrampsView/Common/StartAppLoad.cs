﻿namespace GrampsView.Common
{
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Services;
    using GrampsView.Views;

    using Prism.Events;

    using System.Threading.Tasks;

    public class StartAppLoad : IStartAppLoad
    {
        private static IDatabaseReloadDisplayService _DatabaseReloadDisplayService = new DatabaseReloadDisplayService();

        private static IFirstRunDisplayService _FirstRunDisplayService = new FirstRunDisplayService();

        private static IEventAggregator _iocEventAggregator;

        private static IWhatsNewDisplayService _WhatsNewDisplayService = new WhatsNewDisplayService();

        public StartAppLoad(IEventAggregator iocEventAggregator, FirstRunDisplayService iocFirstRunDisplayService, WhatsNewDisplayService iocWhatsNewDisplayService, DatabaseReloadDisplayService iocDatabaseReloadDisplayService)

        {
            _iocEventAggregator = iocEventAggregator;

            _WhatsNewDisplayService = iocWhatsNewDisplayService;

            _FirstRunDisplayService = iocFirstRunDisplayService;

            _DatabaseReloadDisplayService = iocDatabaseReloadDisplayService;
        }

        public async Task StartProcessing()
        {
            if (DataStore.Instance.DS.IsDataLoaded)
            {
                // CommonRoutines.NavigateHub();
                return;
            }

            if (await _FirstRunDisplayService.ShowIfAppropriate())
            {
                return;
            }

            if (await _DatabaseReloadDisplayService.ShowIfAppropriate())
            {
                CommonLocalSettings.DataSerialised = false;

                return;
            }

            if (await _WhatsNewDisplayService.ShowIfAppropriate())
            {
                return;
            }

            if (CommonLocalSettings.DataSerialised)
            {
                _iocEventAggregator.GetEvent<DataLoadStartEvent>().Publish();
                return;
            }

            // No Serialised Data and made it this far so some problem has occurred. Load everything
            // from the beginning.
            await CommonRoutines.NavigateAsync(nameof(FileInputHandlerPage));
        }
    }
}