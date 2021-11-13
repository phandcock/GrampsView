namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using Microsoft.Toolkit.Mvvm.Messaging;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class PlaceListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public PlaceListViewModel(ICommonLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Place List";
            BaseTitleIcon = CommonConstants.IconPlace;
        }

        public Group<HLinkPlaceModelCollection> PlaceSource
        {
            get
            {
                return DV.PlaceDV.GetAllAsGroupedCardGroup();
            }
        }
    }
}