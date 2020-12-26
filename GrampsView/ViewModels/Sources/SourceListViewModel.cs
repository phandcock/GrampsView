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
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

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
        public SourceListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Source List";
            BaseTitleIcon = CommonConstants.IconSource;
        }

        public CardGroupBase<HLinkSourceModel> SourceSource
        {
            get
            {
                return DV.SourceDV.GetAllAsCardGroupBase();
            }
        }

        public override void PopulateViewModel()
        {
            BaseTitle = "Source List";
            BaseTitleIcon = CommonConstants.IconSource;
        }
    }
}