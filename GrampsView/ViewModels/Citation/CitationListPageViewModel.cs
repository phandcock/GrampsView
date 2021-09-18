namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using Prism.Events;

    public class CitationListViewModel : ViewModelBase
    {
        public CitationListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
                                    : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Citation List";
            BaseTitleIcon = CommonConstants.IconCitation;
        }

        public Group<HLinkCitationModelCollection> CitationSource
        {
            get
            {
                return DV.CitationDV.GetAllAsGroupedCardGroup();
            }
        }
    }
}