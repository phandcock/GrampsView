// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.Interfaces;
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
                if (m.Value == null)
                {
                    return;
                }

                LoadXMLUIItems(true);
            });
        }

        /// <summary>
        /// Loads the thumbnails etc on the UI thread due to limitations with BitMapImage in
        /// Background threads.
        /// </summary>
        /// <param name="notUsed">
        /// The not used.
        /// </param>
        public async void LoadXMLUIItems(object notUsed)
        {
            _CommonLogging.RoutineEntry("LoadXMLUIItems");

            _CommonLogging.DataLogEntryAdd("Organising data after load");
            {
                {
                    // Called in order of media linkages from Media outwards

                    _ = await OrganiseMediaRepository().ConfigureAwait(false);

                    _ = await OrganiseSourceRepository().ConfigureAwait(false);

                    _ = await OrganiseCitationRepository().ConfigureAwait(false);

                    _ = await OrganiseEventRepository().ConfigureAwait(false);

                    _ = await OrganiseFamilyRepository().ConfigureAwait(false);

                    _ = await OrganiseHeaderRepository().ConfigureAwait(false);

                    _ = await OrganiseNameMapRepository().ConfigureAwait(false);

                    _ = OrganiseNoteRepository();

                    _ = await OrganisePlaceRepository().ConfigureAwait(false);

                    _ = await OrganiseRepositoryRepository().ConfigureAwait(false);

                    _ = await OrganiseTagRepository().ConfigureAwait(false);

                    _ = await OrganiseAddressRepository().ConfigureAwait(false);

                    _ = await OrganisePersonNameRepository().ConfigureAwait(false);

                    // People last as they pretty much depend on everything else
                    _ = await OrganisePersonRepository().ConfigureAwait(false);

                    // Apart from BookMarks
                    _ = await OrganiseBookMarkRepository().ConfigureAwait(false);

                    // Final cleanup pending use of some sort of dependency graph on the whole thing
                    _ = await OrganiseMisc().ConfigureAwait(false);

                    Ioc.Default.GetRequiredService<IMessenger>().Send(new NavigationPopRootEvent(true));
                }
            }

            //    _CommonLogging.DataLogEntryAdd(null);

            _CommonLogging.DataLogEntryAdd("Load XML UI Complete - Data ready for display");

            // save the data in a serial format for next time
            _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new DataSaveSerialEvent(true));

            // let everybody know we have finished loading data
            _ = Ioc.Default.GetRequiredService<IMessenger>().Send(new DataLoadCompleteEvent(true));

            _CommonLogging.RoutineExit(nameof(LoadXMLUIItems));
        }
    }
}