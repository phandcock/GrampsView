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

                // Trigger refresh of View fields via INotifyPropertyChanged
                RaisePropertyChanged(string.Empty);

                // Get Header details
                //CardGroup t = new CardGroup { Title = "Header Details" };

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

                //BaseDetail.Add(t);

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