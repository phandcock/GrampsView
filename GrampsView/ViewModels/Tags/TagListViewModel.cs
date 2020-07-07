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
    public class TagListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Prism Event Aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// Prism Navigation Service.
        /// </param>
        public TagListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Tag List";
            BaseTitleIcon = CommonConstants.IconTag;
        }

        /// <summary>
        /// Gets a Caar Group of Tags for display
        /// </summary>
        /// <value>
        /// The tag source.
        /// </value>
        public CardGroup TagSource
        {
            get
            {
                return DV.TagDV.GetAllAsCardGroup();
            }
        }
    }
}