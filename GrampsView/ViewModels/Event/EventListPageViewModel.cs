﻿namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    public class EventListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventListViewModel"/> class.
        /// </summary>

        public EventListViewModel()

        {
            BaseTitle = "Event List";
            BaseTitleIcon = CommonConstants.IconEvents;
        }

        public CardGroup EventSource
        {
            get
            {
                return DV.EventDV.GetAllAsGroupedCardGroup();
            }
        }
    }
}