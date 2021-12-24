namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Events;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Errors;
    using SharedSharp.Logging;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    /// <summary>
    /// Creates a collection of entities with content read from a GRAMPS XML file.
    /// </summary>
    public partial class StorePostLoad : ObservableObject, IStorePostLoad
    {
        /// <summary>
        /// The local common logging.
        /// </summary>
        private readonly ISharedLogging _CommonLogging;

        private readonly IErrorNotifications _commonNotifications;

        /// <summary>
        /// Gets or sets injected Event Aggregator.
        /// </summary>
        private readonly IMessenger _EventAggregator;

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
        public StorePostLoad(ISharedLogging iocCommonLogging, IErrorNotifications iocCommonNotifications, IMessenger iocEventAggregator)
        {
            _EventAggregator = iocEventAggregator;
            _CommonLogging = iocCommonLogging;
            _commonNotifications = iocCommonNotifications;

            _iocPlatformSpecific = DependencyService.Get<IPlatformSpecific>();

            App.Current.Services.GetService<IMessenger>().Register<DataLoadXMLEvent>(this, (r, m) =>
            {
                if (m.Value == null)
                    return;

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

            _commonNotifications.DataLogEntryAdd("Organising data after load");
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

                    await _commonNotifications.DataLogHide();
                }
            }

            _commonNotifications.DataLogEntryAdd(null);

            _commonNotifications.DataLogEntryAdd("Load XML UI Complete - Data ready for display");

            // save the data in a serial format for next time
            App.Current.Services.GetService<IMessenger>().Send(new DataSaveSerialEvent(true));

            // let everybody know we have finished loading data
            App.Current.Services.GetService<IMessenger>().Send(new DataLoadCompleteEvent(true));

            _CommonLogging.RoutineExit(nameof(LoadXMLUIItems));
        }
    }
}