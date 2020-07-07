//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="PlaceDetailPageViewModel.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;
    using Prism.Navigation;

    /// <summary>
    /// Defines the Place Detail Page View ViewModel.
    /// </summary>
    public class PlaceDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The local place object.
        /// </summary>
        private PlaceModel _PlaceObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        /// <param name="iocNavigationService">
        /// </param>
        public PlaceDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator, INavigationService iocNavigationService)
            : base(iocCommonLogging, iocEventAggregator, iocNavigationService)
        {
        }

        /// <summary>
        /// Gets or sets the public Place ViewModel.
        /// </summary>

        public PlaceModel PlaceObject
        {
            get
            {
                return _PlaceObject;
            }

            set
            {
                SetProperty(ref _PlaceObject, value);
            }
        }

        /// <summary>
        /// Handles navigation in wards and sets up the place model parameter.
        /// </summary>
        public override void PopulateViewModel()
        {
            PlaceObject = DV.PlaceDV.GetModelFromHLink(BaseNavParamsHLink);

            if (PlaceObject != null)
            {
                BaseTitle = PlaceObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconPlace;

                // Get Header details
                CardGroup t = new CardGroup { Title = "Header Details" };

                t.Add(new CardListLineCollection
                    {
                        new CardListLine("Card Type:", "Place Detail"),
                        new CardListLine("Title:", PlaceObject.GPTitle),
                        new CardListLine("Name:", PlaceObject.GName),
                        new CardListLine("Type:", PlaceObject.GType),
                    });

                t.Add(DV.PlaceDV.GetModelInfoFormatted(PlaceObject));

                BaseDetail.Add(t);

                // Details
                BaseDetail.Add(PlaceObject.GPlaceRefCollection.GetCardGroup());
                BaseDetail.Add(PlaceObject.PlaceChildCollection.GetCardGroup());

                BaseDetail.Add(PlaceObject.GCitationRefCollection.GetCardGroup());
                BaseDetail.Add(PlaceObject.GTagRefCollection.GetCardGroup());
                BaseDetail.Add(PlaceObject.GURLCollection);

                BaseDetail.Add(PlaceObject.BackHLinkReferenceCollection.GetCardGroup());
            }
        }
    }
}