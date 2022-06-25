namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using Microsoft.Toolkit.Mvvm.Messaging;

    using SharedSharp.Logging;

    /// <summary>
    /// Creates a Family Section Page View ViewModel.
    /// </summary>
    public class FamilyListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// Injected Navigation Service.
        /// </param>
        public FamilyListViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "Family List";
            BaseTitleIcon = CommonConstants.IconFamilies;
        }

        public Group<HLinkFamilyModelCollection> FamilySource
        {
            get
            {
                return DV.FamilyDV.GetAllAsGroupedCardGroup();
            }
        }
    }
}