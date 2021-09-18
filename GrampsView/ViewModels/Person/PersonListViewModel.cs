namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using Prism.Events;

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
        public PersonListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Person List";
            BaseTitleIcon = CommonConstants.IconPeople;
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