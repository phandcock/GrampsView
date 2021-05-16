namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System;

    /// <summary>
    /// Defines the Place Detail Page View ViewModel.
    /// </summary>
    public class PlaceDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public PlaceDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
        }

        /// <summary>
        /// Gets or sets the public Place ViewModel.
        /// </summary>

        public PlaceModel PlaceObject
        {
            get; set;
        }

        /// <summary>
        /// Handles navigation inwards and sets up the place model parameter.
        /// </summary>
        public override void BaseHandleLoadEvent()
        {
            HLinkPlaceModel HLinkObject = CommonRoutines.DeserialiseObject<HLinkPlaceModel>(Uri.UnescapeDataString(BaseParamsHLink));

            PlaceObject = HLinkObject.DeRef;

            if (PlaceObject != null)
            {
                BaseTitle = PlaceObject.GetDefaultText;
                BaseTitleIcon = CommonConstants.IconPlace;

                BaseDetail.Add(new CardListLineCollection("Place Detail")
                    {
                        new CardListLine("Title:", PlaceObject.GPTitle),
                        new CardListLine("Name:", PlaceObject.GPlaceNames.GetDefaultText),
                        new CardListLine("Type:", PlaceObject.GType),
                        new CardListLine("Code:", PlaceObject.GCode),
                });

                // Get Coordinates TODO Setup Map link card button thing t = new CardGroup { Title =
                // "Coordinates" };

                BaseDetail.Add(new CardListLineCollection
                    {
                        new CardListLine("Lat:", PlaceObject.GCoordLat),
                        new CardListLine("Long:", PlaceObject.GCoordLong),
                });

                BaseDetail.Add(DV.PlaceDV.GetModelInfoFormatted(PlaceObject));

                // Get Place Name details
                if (PlaceObject.GPlaceNames.Count > 1)
                {
                    //t = new CardGroup { Title = "Place Names" };

                    //foreach (PlaceNameModel item in PlaceObject.GPlaceNames)
                    //{
                    //    t.Add(new CardListLineCollection
                    //    {
                    //        new CardListLine("Card Type:", "Place Name"),
                    //        new CardListLine("Name:", item.GValue),
                    //        new CardListLine("Language:", item.GLang),
                    //        new CardListLine("Date:", item.GDate.ShortDate),
                    //    });
                    //}

                    // TODO ? above

                    BaseDetail.Add(PlaceObject.GPlaceNames);
                }
            }
        }
    }
}