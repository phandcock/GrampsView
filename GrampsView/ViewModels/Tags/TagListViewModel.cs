﻿namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

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
        public TagListViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
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
        public CardGroupBase<HLinkTagModel> TagSource
        {
            get
            {
                return DV.TagDV.GetAllAsCardGroupBase();
            }
        }
    }
}