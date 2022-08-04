namespace GrampsView.ViewModels
{
    using CommunityToolkit.Mvvm.Messaging;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using SharedSharp.Logging;

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
        public TagListViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "Tag List";
            BaseTitleIcon = Constants.IconTag;
        }

        /// <summary>
        /// Gets a Caar Group of Tags for display
        /// </summary>
        /// <value>
        /// The tag source.
        /// </value>
        public CardGroupHLink<HLinkTagModel> TagSource
        {
            get
            {
                return DV.TagDV.GetAllAsCardGroupBase();
            }
        }
    }
}