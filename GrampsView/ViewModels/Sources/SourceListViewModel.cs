//-
namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;

    /// <summary>
    /// View Model for the Event Section Page.
    /// </summary>
    public class SourceListViewModel : ViewModelBase
    {
        public Group<HLinkSourceModelCollection> SourceSource
        {
            get
            {
                return DV.SourceDV.GetAllAsGroupedCardGroup();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public SourceListViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
            BaseTitle = "Source List";
            BaseTitleIcon = CommonConstants.IconSource;
        }
    }
}