namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    using System.Linq;

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
        /// <param name="iocNavigationService">
        /// </param>
        /// <param name="iocPlatformSpecific">
        /// </param>
        public FamilyDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService, IPlatformSpecific iocPlatformSpecific)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
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
        public override void PopulateViewModel()
        {
            FamilyObject = DV.FamilyDV.GetModelFromHLink(BaseNavParamsHLink);

            if (!(FamilyObject is null))
            {
                BaseTitle = FamilyObject.FamilyDisplayName;
                BaseTitleIcon = CommonConstants.IconFamilies;

                // Trigger refresh of View fields via INotifyPropertyChanged
                RaisePropertyChanged(string.Empty);

                // Get basic details

                CardListLineCollection t1 = new CardListLineCollection
                    {
                    new CardListLine("Family Display Name:", FamilyObject.FamilyDisplayName),
                    new CardListLine("Family Relationship:", FamilyObject.GFamilyRelationship),
                    new CardListLine("Father Name:", FamilyObject.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.FullName),
                    new CardListLine("Mother Name:", FamilyObject.GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.FullName),
                };

                t1.Title = "Family Detail";

                BaseDetail.Add(t1);

                // Add Model details
                BaseDetail.Add(DV.FamilyDV.GetModelInfoFormatted(FamilyObject));

                // Add parent link
                BaseDetail.Add(new ParentLinkModel
                {
                    Parents = localFamilyModel,
                });

                string outFamEvent;
                if (FamilyObject.GEventRefCollection.Count > 0)
                {
                    // TODO Handle this
                    outFamEvent = FamilyObject.GEventRefCollection.FirstOrDefault().DeRef.GType + ": " + FamilyObject.GEventRefCollection.FirstOrDefault().DeRef.GDate.ShortDate;
                }

                _PlatformSpecific.ActivityTimeLineAdd(FamilyObject);
            }
        }
    }
}