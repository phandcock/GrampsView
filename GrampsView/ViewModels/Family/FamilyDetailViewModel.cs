namespace GrampsView.ViewModels
{
    using CommunityToolkit.Mvvm.Messaging;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using SharedSharp.Logging;
    using SharedSharp.Model;

    /// <summary>
    /// Family detail page view ViewModel.
    /// </summary>
    public class FamilyDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Holds the family object.
        /// </summary>
        private FamilyModel localFamilyModel = new FamilyModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Prism event aggregator.
        /// </param>
        /// <param name="iocPlatformSpecific">
        /// Platform specific calls for Windows Timeline
        /// </param>
        public FamilyDetailViewModel(ISharedLogging iocCommonLogging, IMessenger iocEventAggregator)
                                    : base(iocCommonLogging)
        {
        }

        /// <summary>
        /// Gets or sets the Family object.
        /// </summary>
        /// <value>
        /// The family object.
        /// </value>
        public FamilyModel FamilyObject
        {
            get
            {
                return localFamilyModel;
            }

            set
            {
                SetProperty(ref localFamilyModel, value);
            }
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// Task.
        /// </returns>
        public override void HandleViewDataLoadEvent()
        {
            HLinkFamilyModel HLinkFamily = CommonRoutines.GetHLinkParameter<HLinkFamilyModel>(BaseParamsHLink);

            FamilyObject = HLinkFamily.DeRef;

            if (!(FamilyObject is null))
            {
                BaseModelBase = FamilyObject;
                BaseTitleIcon = Constants.IconFamilies;

                // Get basic details
                BaseDetail.Add(new CardListLineCollection("Family Detail")
                    {
                    new CardListLine("Family Display Name:", FamilyObject.ToString()),
                    new CardListLine("Family Relationship:", FamilyObject.GFamilyRelationship),
                    new CardListLine("Father Name:", FamilyObject.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.FullName),
                    new CardListLine("Mother Name:", FamilyObject.GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.FullName),
                    new CardListLine("Date:",FamilyObject.GDate.LongDate),
                });

                // Add Model details
                BaseDetail.Add(DV.FamilyDV.GetModelInfoFormatted(FamilyObject));

                // Add parent link
                BaseDetail.Add(new HLinkParentLinkModel
                {
                    DeRef = localFamilyModel,
                });
            }
        }
    }
}