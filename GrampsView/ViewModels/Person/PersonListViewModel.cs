namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using CommunityToolkit.Mvvm.Messaging;

    using SharedSharp.Logging;
    using GrampsView.Models.Collections.HLinks;

    /// <summary>
    /// People Page View ViewModel.
    /// </summary>
    public class PersonListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public PersonListViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "Person List";
            BaseTitleIcon = Constants.IconPeople;
        }

        /// <summary>
        /// Gets the person data view.
        /// </summary>
        /// <value>
        /// The person data view.
        /// </value>
        public Group<HLinkPersonModelCollection> PersonSource
        {
            get
            {
                return DV.PersonDV.GetAllAsGroupedCardGroup();
            }
        }
    }
}