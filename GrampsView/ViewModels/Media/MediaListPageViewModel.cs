using CommunityToolkit.Mvvm.Messaging;

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;

using GrampsView.ViewModels;

namespace GrampsView.ViewModels.Media
{
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
        public MediaListViewModel(ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "Media List";
            BaseTitleIcon = Constants.IconMedia;
        }

        public Group<HLinkMediaModelCollection> MediaSource => DV.MediaDV.GetAllAsGroupedCardGroup();
    }
}