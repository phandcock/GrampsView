// <copyright file="GrampsStoreSerialPostLoad.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad
    {
        /// <summary>
        /// Loads the UI items.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
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