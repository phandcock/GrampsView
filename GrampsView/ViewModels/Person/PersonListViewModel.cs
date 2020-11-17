namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using Prism.Events;
    using Prism.Navigation;

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
        public PersonListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
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
        public CardGroup PersonSource
        {
            get
            {
                return DV.PersonDV.GetAllAsGroupedSurnameCardGroup();
            }
        }
    }
}