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
    public class RepositoryListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The Common Logger
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// The ioc navigation service.
        /// </param>
        public RepositoryListViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "Repository List";
            BaseTitleIcon = CommonConstants.IconRepository;
        }

        public HLinkRepositoryModelCollection RepositorySource
        {
            get
            {
                return DV.RepositoryDV.GetAllAsCardGroupBase();
            }
        }
    }
}