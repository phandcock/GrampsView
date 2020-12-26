namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// View Model for the Citation Section Page.
    /// </summary>
    public class CitationListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationListViewModel"/> class.
        /// </summary>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public CitationListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
                                    : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Citation List";
            BaseTitleIcon = CommonConstants.IconCitation;
        }

        public CardGroupBase<HLinkCitationModel> CitationSource
        {
            get
            {
                return DV.CitationDV.GetAllAsCardGroupBase();
            }
        }
    }
}