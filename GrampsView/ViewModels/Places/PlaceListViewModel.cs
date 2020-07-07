//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="PlaceListViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using Prism.Events;
    using Prism.Navigation;
    using System.Collections.ObjectModel;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class PlaceListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public PlaceListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Place List";
            BaseTitleIcon = CommonConstants.IconPlace;
        }

        public CardGroup PlaceSource
        {
            get
            {
                return DV.PlaceDV.GetAllAsCardGroup();
            }
        }
    }
}