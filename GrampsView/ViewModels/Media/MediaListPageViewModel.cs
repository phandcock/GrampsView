namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using CommunityToolkit.Mvvm.Messaging;

    using SharedSharp.Logging;

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
        public MediaListViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "Media List";
            BaseTitleIcon = Constants.IconMedia;
        }

        public Group<HLinkMediaModelCollection> MediaSource
        {
            get
            {
                return DV.MediaDV.GetAllAsGroupedCardGroup();
            }
        }

        public override async Task HandleViewAppearingEvent()
        {
            return;
        }
    }
}