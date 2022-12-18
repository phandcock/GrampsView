using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;

namespace GrampsView.ViewModels.Family
{
    /// <summary>
    /// Creates a Family Section Page View ViewModel.
    /// </summary>
    public class FamilyListViewModel : ViewModelBase
    {
        /// <summary>Initializes a new instance of the <see cref="FamilyListViewModel" /> class.</summary>
        /// <param name="iocCommonLogging">The common logging.</param>
        public FamilyListViewModel(ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitle = "Family List";
            BaseTitleIcon = Constants.IconFamilies;
        }

        public Group<HLinkFamilyModelCollection> FamilySource => DV.FamilyDV.GetAllAsGroupedCardGroup();
    }
}