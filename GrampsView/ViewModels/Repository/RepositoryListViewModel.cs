//-----------------------------------------------------------------------
// Code behind for Repository List page
//
// <copyright file="RepositoryListViewModel.cs" company="MeMyselfAndI">
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
    public class RepositoryListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The Common Logger
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// The ioc navigation service.
        /// </param>
        public RepositoryListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Repository List";
            BaseTitleIcon = CommonConstants.IconRepository;
        }

        public CardGroup RepositorySource
        {
            get
            {
                return DV.RepositoryDV.GetAllAsCardGroup();
            }
        }
    }
}