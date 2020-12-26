namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using Prism.Events;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class BookMarkListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookMarkListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// Prism Navigation Service.
        /// </param>
        public BookMarkListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "BookMark List";
            BaseTitleIcon = CommonConstants.IconBookMark;
        }

        public CardGroup BookMarkSource
        {
            get
            {
                return DV.BookMarkCollection.CardGroupAsProperty;
            }
        }
    }
}