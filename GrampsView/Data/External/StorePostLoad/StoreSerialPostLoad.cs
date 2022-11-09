using System.Threading.Tasks;

namespace GrampsView.Data.ExternalStorage
{
    public partial class StorePostLoad
    {
        public async Task LoadSerialUiItems()
        {
            _CommonLogging.RoutineEntry("LoadSerialUiItems");

            _CommonLogging.DataLogEntryAdd("Organising data after load");
            {
                _CommonLogging.RoutineExit(string.Empty);

                _ = await FixMediaFiles().ConfigureAwait(false);
            }

            _CommonLogging.DataLogEntryAdd("Serial UI Load Complete. Data ready for display");

            _CommonLogging.RoutineExit(nameof(LoadSerialUiItems));
        }
    }
}