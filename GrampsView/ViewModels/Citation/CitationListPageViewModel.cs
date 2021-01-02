namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    public class CitationListViewModel : ViewModelBase
    {
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