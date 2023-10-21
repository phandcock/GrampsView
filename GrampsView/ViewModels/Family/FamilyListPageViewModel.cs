﻿using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Models.Collections.HLinks;

namespace GrampsView.ViewModels.Family
{
    /// <summary>
    /// Creates a Family Section Page View ViewModel.
    /// </summary>
    public class FamilyListViewModel : ViewModelBase
    {
        public Group<HLinkFamilyModelCollection> FamilySource => DL.FamilyDL.GetAllAsGroupedCardGroup();

        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyListViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging.
        /// </param>
        public FamilyListViewModel(ILog iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitle = "Family List";
            BaseTitleIcon = Constants.IconFamilies;
        }
    }
}