// Copyright (c) phandcock.  All rights reserved.

using CommunityToolkit.Mvvm.Messaging;

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.ModelsDB.Collections.HLinks;

using SharedSharp.Logging;

namespace GrampsView.ViewModels
{
    public class CitationListViewModel : ViewModelBase
    {
        public CitationListViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator)
                                    : base(iocCommonLogging)
        {
            BaseTitle = "Citation List";
            BaseTitleIcon = Constants.IconCitation;
        }

        public Group<HLinkCitationDBModelCollection> CitationSource
        {
            get
            {
                return DL.CitationDL.GetAllAsGroupedCardGroup();
            }
        }
    }
}