//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="EventListPageViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using System.Collections.ObjectModel;

    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class EventListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventListViewModel"/> class.
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
        public EventListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Event List";
            BaseTitleIcon = CommonConstants.IconEvents;
        }

        public CardGroup EventSource
        {
            get
            {
                return DV.EventDV.GetAllAsCardGroup();
            }
        }
    }
}