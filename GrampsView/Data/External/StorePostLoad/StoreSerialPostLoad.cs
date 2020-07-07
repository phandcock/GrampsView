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
            _CommonLogging.LogRoutineEntry("LoadSerialUiItems");

            await DataStore.CN.ChangeLoadingMessage("Organising data after load").ConfigureAwait(false);
            {
                _CommonLogging.LogRoutineExit(string.Empty);

                await FixMediaFiles().ConfigureAwait(false);
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            await DataStore.CN.MinorStatusAdd("Serial UI Load Complete. Data ready for display").ConfigureAwait(false);

            _CommonLogging.LogRoutineExit(nameof(LoadSerialUiItems));
        }
    }
}