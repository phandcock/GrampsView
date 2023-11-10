// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.ModelsDB.Collections.HLinks;

namespace GrampsView.ViewModels
{
    public class EventListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventListViewModel"/> class.
        /// </summary>

        public EventListViewModel(ILog iocCommonLogging)
             : base(iocCommonLogging)
        {
            BaseTitle = "Event List";
            BaseTitleIcon = Constants.IconEvents;
        }

        public Group<HLinkEventDBModelCollection> EventSource => DL.EventDL.GetAllAsGroupedCardGroup();
    }
}