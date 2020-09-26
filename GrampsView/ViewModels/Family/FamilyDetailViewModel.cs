//-----------------------------------------------------------------------
//
// Family Detail View Model.
//
// <copyright file="FamilyDetailViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
        /// <summary>
        /// The activity session.
        /// </summary>
        //[NonSerializedAttribute]
        //private UserActivitySession localActivitySession;

        private IPlatformSpecific _PlatformSpecific;

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
        // [RestorableState]
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
        private HLinkMediaModel _MediaCard = new HLinkMediaModel();

        public HLinkMediaModel MediaCard
        {
            get
            {
                return _MediaCard;
            }
            set
            {
                SetProperty(ref _MediaCard, value);
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

            // Trigger refresh of View fields via INotifyPropertyChanged
            RaisePropertyChanged(string.Empty);

            if (!(FamilyObject is null))
            {
                BaseTitle = FamilyObject.FamilyDisplayName;
                BaseTitleIcon = CommonConstants.IconFamilies;

                // Get media image
                MediaCard = FamilyObject.HomeImageHLink.ConvertToHLinkMediaModel;


                // Get basic details
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Add(new CardListLineCollection
            {
                new CardListLine("Card Type:", "Family Detail"),
                new CardListLine("Family Display Name:", FamilyObject.FamilyDisplayName),
                new CardListLine("Family Relationship:", FamilyObject.GFamilyRelationship),
                new CardListLine("Father Name:", FamilyObject.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.FullName),
                new CardListLine("Mother Name:", FamilyObject.GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.FullName),
            });

                // Add Model details
                t.Add(DV.FamilyDV.GetModelInfoFormatted(FamilyObject));

                // Add parent link
                t.Add(new ParentLinkModel
                {
                    Parents = localFamilyModel,
                });

                BaseDetail.Add(t);

                // Detail reference
                BaseDetail.Add(FamilyObject.GEventRefCollection.GetCardGroup());
                BaseDetail.Add(FamilyObject.GChildRefCollection.GetCardGroup()); // TODO , "Children");

                BaseDetail.Add(FamilyObject.GCitationRefCollection.GetCardGroup());
                BaseDetail.Add(FamilyObject.GMediaRefCollection.GetCardGroup());
                BaseDetail.Add(FamilyObject.GNoteRefCollection.GetCardGroup());
                BaseDetail.Add(FamilyObject.GAttributeCollection);
                BaseDetail.Add(FamilyObject.GTagRefCollection.GetCardGroup());

                BaseDetail.Add(FamilyObject.BackHLinkReferenceCollection.GetCardGroup());

                string outFamEvent;
                if (FamilyObject.GEventRefCollection.Count > 0)
                {
                    // TODO Handle this
                    outFamEvent = FamilyObject.GEventRefCollection.FirstOrDefault().DeRef.GType + ": " + FamilyObject.GEventRefCollection.FirstOrDefault().DeRef.GDate.ShortDate;
                }

                // TODO localActivitySession = await CommonTimeline.AddToTimeLine("Family",
                // localFamilyModel, localFamilyModel.HomeImageHLink.DeRef.MediaStorageFilePath,
                // "Family: " + localFamilyModel.FamilyDisplayName, outFamEvent).ConfigureAwait(false);

                // TODO //CommonTimeline.FinishActivitySessionAsync(localActivitySession); }

                _PlatformSpecific.ActivityTimeLineAdd(FamilyObject);
            }
        }
    }
}