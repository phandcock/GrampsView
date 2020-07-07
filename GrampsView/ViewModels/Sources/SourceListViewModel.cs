//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="SourceListViewModel.cs" company="MeMyselfAndI">
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
    public class SourceListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public SourceListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Source List";
            BaseTitleIcon = CommonConstants.IconSource;
        }

        public CardGroup SourceSource
        {
            get
            {
                return DV.SourceDV.GetAllAsCardGroup();
            }
        }

        public override void PopulateViewModel()
        {
            BaseTitle = "Source List";
            BaseTitleIcon = CommonConstants.IconSource;
        }
    }
}