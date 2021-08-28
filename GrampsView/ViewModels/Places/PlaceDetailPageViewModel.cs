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
            HLinkPlaceModel HLinkObject = CommonRoutines.GetHLinkParameter<HLinkPlaceModel>(BaseParamsHLink);

            PlaceObject = HLinkObject.DeRef;

            if (PlaceObject != null)
            {
                BaseModelBase = PlaceObject;
                BaseTitleIcon = CommonConstants.IconPlace;

                // TODO Display all details

                BaseDetail.Add(new CardListLineCollection("Place Detail")
                    {
                        new CardListLine("Title:", PlaceObject.GPTitle),
                        new CardListLine("Type:", PlaceObject.GType),
                        new CardListLine("Code:", PlaceObject.GCode),
                });

                foreach (PlaceLocationModel thePlaceLocation in PlaceObject.GLocation)
                {
                    BaseDetail.Add(new CardListLineCollection("Location")
                    {
                        new CardListLine("Street:", thePlaceLocation.GStreet),
                        new CardListLine("City:", thePlaceLocation.GCity),
                        new CardListLine("County:", thePlaceLocation.GCounty),
                        new CardListLine("Locality:", thePlaceLocation.GLocality),
                        new CardListLine("Parish:", thePlaceLocation.GParish),
                        new CardListLine("State:", thePlaceLocation.GState),
                        new CardListLine("Country:", thePlaceLocation.GCountry),
                        new CardListLine("Phone:", thePlaceLocation.GPhone),
                        new CardListLine("City:", thePlaceLocation.GPostal),
                    });
                }

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
                IMapModel t = PlaceObject.ToMapModel();
                BaseDetail.Add(t.HLink);

                BaseDetail.Add(DV.PlaceDV.GetModelInfoFormatted(PlaceObject));
            }
        }
    }
}