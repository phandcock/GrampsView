// Copyright (c) phandcock. All rights reserved.

using GrampsView.Common.Interfaces;
using GrampsView.Data.StorePostLoad;
using GrampsView.Events;

using SharedSharp.Errors.Interfaces;

namespace GrampsView.Data.ExternalStorage
{
    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : ObservableObject, IStorePostLoad
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ILog _CommonLogging;

        private readonly IErrorNotifications _commonNotifications;

        /// <summary>
        /// Gets or sets injected Event Aggregator.
        /// </summary>
        private readonly IMessenger _EventAggregator;

        private readonly IGenerateThumbnails MyGenerateThumbNails = new Common.CustomClasses.GenerateThumbNails();

        /// <summary>
        /// Initializes a new instance of the <see cref="StorePostLoad"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocCommonNotifications">
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public StorePostLoad(ILog iocCommonLogging, IErrorNotifications iocCommonNotifications, IMessenger iocEventAggregator)
        {
            _EventAggregator = iocEventAggregator;
            _CommonLogging = iocCommonLogging;
            _commonNotifications = iocCommonNotifications;

            Ioc.Default.GetRequiredService<IMessenger>().Register<DataLoadXMLEvent>(this, (r, m) =>
            {
                LoadXMLUIItems(m.Value);
            });
        }

        /// <summary>
        /// Loads the thumbnails etc on the UI thread due to limitations with BitMapImage in
        /// Background threads.
        /// </summary>
        /// <param name="notUsed">
        /// The not used.
        /// </param>
        public void LoadXMLUIItems(object notUsed)
        {
            _CommonLogging.RoutineEntry("LoadXMLUIItems");

            _CommonLogging.DataLogEntryAdd("Organising data after load");
            {
                {
                    // Called in order of media linkages from Media outwards

                    _ = OrganiseMediaRepository();

                    _ = OrganiseSourceRepository();

                    _ = OrganiseCitationRepository();

                    _ = OrganiseEventRepository();

                    _ = OrganiseFamilyRepository();

                    _ = OrganiseHeaderRepository();

                    _ = OrganiseNameMapRepository();

                    _ = OrganiseNoteRepository();

                    _ = OrganisePlaceRepository();

                    _ = OrganiseRepositoryRepository();

                    _ = OrganiseTagRepository();

                    _ = OrganiseAddressRepository();

                    _ = OrganisePersonNameRepository();

                    // People last as they pretty much depend on everything else
                    _ = OrganisePersonRepository();

                    // Apart from BookMarks
                    _ = OrganiseBookMarkRepository();

                    // Final cleanup pending use of some sort of dependency graph on the whole thing
                    _ = OrganiseMisc();
                }
            }

            // _CommonLogging.DataLogEntryAdd(null);

            _CommonLogging.DataLogEntryAdd("Load XML UI Complete - Data ready for display");

            // save the data in a serial format for next time
            _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new DataSaveSerialEvent(true));

            // let everybody know we have finished loading data
            _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new DataLoadCompleteEvent(true));

            _CommonLogging.RoutineExit(nameof(LoadXMLUIItems));
        }
    }
}