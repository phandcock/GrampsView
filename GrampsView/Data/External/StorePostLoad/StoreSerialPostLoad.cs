namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Data.Repository;

    using System.Threading.Tasks;

    public partial class StorePostLoad
    {
        public async Task LoadSerialUiItems()
        {
            _CommonLogging.RoutineEntry("LoadSerialUiItems");

            await DataStore.Instance.CN.DataLogEntryAdd("Organising data after load").ConfigureAwait(false);
            {
                _CommonLogging.RoutineExit(string.Empty);

                await FixMediaFiles().ConfigureAwait(false);
            }

            await DataStore.Instance.CN.DataLogEntryAdd("Serial UI Load Complete. Data ready for display").ConfigureAwait(false);

            _CommonLogging.RoutineExit(nameof(LoadSerialUiItems));
        }
    }
}