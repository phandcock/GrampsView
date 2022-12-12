using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;

namespace GrampsView.ViewModels.Repository
{
    /// <summary>
    /// View Model for the Repository Section Page.
    /// </summary>
    public class RepositoryListViewModel : ViewModelBase
    {
        /// <summary>Initializes a new instance of the <see cref="RepositoryListViewModel" /> class.</summary>
        /// <param name="iocCommonLogging">The Common Logger</param>
        /// <param name="iocEventAggregator">The ioc event aggregator.</param>
        public RepositoryListViewModel(ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitle = "Repository List";
            BaseTitleIcon = Constants.IconRepository;
        }

        public HLinkRepositoryModelCollection RepositorySource => DV.RepositoryDV.GetAllAsCardGroupBase();
    }
}