namespace GrampsView.Common
{
    using GrampsView.Data.Repository;
    using GrampsView.Events;
    using GrampsView.Services;
    using GrampsView.Views;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;

    using System.Threading.Tasks;

    public class StartAppLoad : IStartAppLoad
    {
        private static IDatabaseReloadDisplayService _DatabaseReloadDisplayService = new DatabaseReloadDisplayService();

        private static IFirstRunDisplayService _FirstRunDisplayService = new FirstRunDisplayService();

        private static IMessenger _iocEventAggregator;

        private static IWhatsNewDisplayService _WhatsNewDisplayService = new WhatsNewDisplayService();

        public StartAppLoad(IMessenger iocEventAggregator, FirstRunDisplayService iocFirstRunDisplayService, WhatsNewDisplayService iocWhatsNewDisplayService, DatabaseReloadDisplayService iocDatabaseReloadDisplayService)

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
                App.Current.Services.GetService<IMessenger>().Send(new DataLoadStartEvent(true));
                return;
            }

            // No Serialised Data and made it this far so some problem has occurred. Load everything
            // from the beginning.
            await SharedSharp.CommonRoutines.CommonRoutines.NavigateAsync(nameof(FileInputHandlerPage));
        }
    }
}