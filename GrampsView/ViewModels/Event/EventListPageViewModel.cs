// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;

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

        public Group<HLinkEventModelCollection> EventSource => DL.EventDL.GetAllAsGroupedCardGroup();
    }
}