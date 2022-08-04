namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using CommunityToolkit.Mvvm.Messaging;

    using SharedSharp.Logging;

    public class CitationListViewModel : ViewModelBase
    {
        public CitationListViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
                                    : base(iocCommonLogging)
        {
            BaseTitle = "Citation List";
            BaseTitleIcon = Constants.IconCitation;
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