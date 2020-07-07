// <copyright file="CitationListPageViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

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
    public class CitationListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationListViewModel"/> class.
        /// </summary>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public CitationListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Citation List";
            BaseTitleIcon = CommonConstants.IconCitation;
        }

        public CardGroup CitationSource
        {
            get
            {
                return DV.CitationDV.GetAllAsCardGroup();
            }
        }
    }
}