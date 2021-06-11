namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;
    using GrampsView.Events;

    using Prism.Events;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : ObservableObject, IStorePostLoad
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ICommonLogging _CommonLogging;

        /// <summary>
        /// Gets or sets injected Event Aggregator.
        /// </summary>
        private readonly IEventAggregator _EventAggregator;

        private readonly IPlatformSpecific _iocPlatformSpecific;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorePostLoad"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public StorePostLoad(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, IPlatformSpecific iocPlatformSpecific)
        {
            _EventAggregator = iocEventAggregator;
            _CommonLogging = iocCommonLogging;
            _iocPlatformSpecific = iocPlatformSpecific;

            _EventAggregator.GetEvent<DataLoadXMLEvent>().Subscribe(LoadXMLUIItems, ThreadOption.UIThread);
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

            await DataStore.Instance.CN.DataLogEntryAdd("Organising data after load").ConfigureAwait(false);
            {
                {
                    // Called in order of media linkages from Media outwards

                    await OrganiseMediaRepository().ConfigureAwait(false);

                    await OrganiseSourceRepository().ConfigureAwait(false);

                    await OrganiseCitationRepository().ConfigureAwait(false);

                    await OrganiseEventRepository().ConfigureAwait(false);

                    await OrganiseFamilyRepository().ConfigureAwait(false);

                    await OrganiseHeaderRepository().ConfigureAwait(false);

                    await OrganiseNameMapRepository().ConfigureAwait(false);

                    await OrganiseNoteRepository().ConfigureAwait(false);

                    await OrganisePlaceRepository().ConfigureAwait(false);

                    await OrganiseRepositoryRepository().ConfigureAwait(false);

                    await OrganiseTagRepository().ConfigureAwait(false);

                    await OrganiseAddressRepository().ConfigureAwait(false);

                    await OrganisePersonNameRepository().ConfigureAwait(false);

                    // People last as they pretty much depend on everything else
                    await OrganisePersonRepository().ConfigureAwait(false);

                    // Apart from BookMarks
                    await OrganiseBookMarkRepository().ConfigureAwait(false);

                    // Final cleanup pending use of some sort of dependency graph on the whole thing
                    await OrganiseMisc().ConfigureAwait(false);
                }
            }

            await DataStore.Instance.CN.DataLogEntryAdd(null).ConfigureAwait(false);

            await DataStore.Instance.CN.DataLogEntryAdd("Load XML UI Complete - Data ready for display").ConfigureAwait(false);

            // save the data in a serial format for next time
            _EventAggregator.GetEvent<DataSaveSerialEvent>().Publish(null);

            // let everybody know we have finished loading data
            _EventAggregator.GetEvent<DataLoadCompleteEvent>().Publish();

            _CommonLogging.RoutineExit(nameof(LoadXMLUIItems));
        }
    }
}