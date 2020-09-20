//-----------------------------------------------------------------------
//
// View model for the MediaObjectSectionPageView
//
// <copyright file="MediaListPageViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// </summary>
    public class MediaListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// The ioc navigation service.
        /// </param>
        public MediaListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
            BaseTitle = "Media List";
            BaseTitleIcon = CommonConstants.IconMedia;
        }

        public CardGroupBase<HLinkMediaModel> MediaSource
        {
            get
            {
                return DV.MediaDV.GetAllAsCardGroup();
            }
        }

        public override void PopulateViewModel()
        {
            return;
        }
    }
}