namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    /// <summary>
    /// Family detail page view ViewModel.
    /// </summary>
    public class FamilyDetailViewModel : ViewModelBase
    {
        private readonly IPlatformSpecific _PlatformSpecific;

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
        public FamilyDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, IPlatformSpecific iocPlatformSpecific)
                                    : base(iocCommonLogging, iocEventAggregator)
        {
            _PlatformSpecific = iocPlatformSpecific;
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
        public override void BaseHandleLoadEvent()
        {
            HLinkFamilyModel HLinkFamily = CommonRoutines.GetHLinkParameter<HLinkFamilyModel>(BaseParamsHLink);

            FamilyObject = HLinkFamily.DeRef;

            if (!(FamilyObject is null))
            {
                BaseModelBase = FamilyObject;
                BaseTitleIcon = CommonConstants.IconFamilies;

                // Get basic details

                BaseDetail.Add(new CardListLineCollection("Family Detail")
                    {
                    new CardListLine("Family Display Name:", FamilyObject.DefaultText),
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

                _PlatformSpecific.ActivityTimeLineAdd(FamilyObject);
            }
        }
    }
}