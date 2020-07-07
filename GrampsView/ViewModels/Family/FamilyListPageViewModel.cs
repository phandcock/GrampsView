//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="FamilyListPageViewModel.cs" company="MeMyselfAndI">
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
    /// Creates a Family Section Page View ViewModel.
    /// </summary>
    public class FamilyListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// Injected Navigation Service.
        /// </param>
        public FamilyListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Family List";
            BaseTitleIcon = CommonConstants.IconFamilies;
        }

        public CardGroup FamilySource
        {
            get
            {
                return DV.FamilyDV.GetAllAsCardGroup();
            }
        }
    }
}