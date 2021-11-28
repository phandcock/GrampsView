namespace GrampsView.Data.ExternalStorage
{
    using System.Threading.Tasks;

    public partial class StorePostLoad
    {
        public async Task LoadSerialUiItems()
        {
            _CommonLogging.RoutineEntry("LoadSerialUiItems");

            await _commonNotifications.DataLogEntryAdd("Organising data after load").ConfigureAwait(false);
            {
                _CommonLogging.RoutineExit(string.Empty);

                await FixMediaFiles().ConfigureAwait(false);
            }

            _commonNotifications.DataLogEntryAdd("Serial UI Load Complete. Data ready for display");

            _CommonLogging.RoutineExit(nameof(LoadSerialUiItems));
        }
    }
}