//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="NameMapListPageViewModel.cs" company="MeMyselfAndI">
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

    public class NameMapListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameMapListViewModel"/> class.
        /// </summary>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        public NameMapListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "NameMap List";
            BaseTitleIcon = CommonConstants.IconNameMaps;
        }

        public CardGroup NameMapSource
        {
            get
            {
                return DV.NameMapDV.GetAllAsCardGroup();
            }
        }
    }
}