namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

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
            HLinkPlaceModel HLinkObject = CommonRoutines.GetHLinkParameter<HLinkPlaceModel>((BaseParamsHLink));

            PlaceObject = HLinkObject.DeRef;

            if (PlaceObject != null)
            {
                BaseModelBase = PlaceObject;
                BaseTitleIcon = CommonConstants.IconPlace;

                // TODO Dispaly all details

                BaseDetail.Add(new CardListLineCollection("Place Detail")
                    {
                        new CardListLine("Title:", PlaceObject.GPTitle),
                        new CardListLine("Type:", PlaceObject.GType),
                        new CardListLine("Code:", PlaceObject.GCode),
                });

                BaseDetail.Add(new CardListLineCollection("Coordinates")
                    {
                        new CardListLine("Lat:", PlaceObject.GCoordLat),
                        new CardListLine("Long:", PlaceObject.GCoordLong),
                  });

                BaseDetail.Add(new CardListLineCollection("Place Date Ref")
                    {
                        new CardListLine("Date:", HLinkObject.Date.ShortDate),
                });

                // Add Map card
                MapModel t = PlaceObject.ToMapModel();
                BaseDetail.Add(t.HLink);

                BaseDetail.Add(DV.PlaceDV.GetModelInfoFormatted(PlaceObject));
            }
        }
    }
}