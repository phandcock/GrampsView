namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// ViewModel for the Address Detail page.
    /// </summary>
    public class AddressDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        public AddressDetailViewModel(ICommonLogging iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitleIcon = CommonConstants.IconAddress;
        }

        /// <summary>
        /// Gets or sets the View Current Person.
        /// </summary>
        /// <value>
        /// The current person ViewModel.
        /// </value>
        public AddressModel AddressObject
        {
            get; set;
        }

        public HLinkMediaModel MediaCard
        {
            get; set;
        }

        /// <summary>
        /// Populates the view ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        public override void BaseHandleLoadEvent()
        {
            BaseCL.RoutineEntry("AddressDetailViewModel");

            HLinkAdressModel HLinkObject = CommonRoutines.GetHLinkParameter<HLinkAdressModel>((BaseParamsHLink));

            AddressObject = HLinkObject.DeRef;

            if (AddressObject.Valid)
            {
                BaseModelBase = AddressObject;

                // Get media image
                MediaCard = AddressObject.ModelItemGlyph.ImageHLinkMediaModel;

                // Get the Name Details
                BaseDetail.Add(new CardListLineCollection("Address Detail")
                {
                    new CardListLine("Street:", AddressObject.GStreet),
                    new CardListLine("City:", AddressObject.GCity),
                    new CardListLine("Locality:", AddressObject.GLocality),
                    new CardListLine("County:", AddressObject.GCounty),
                    new CardListLine("State:", AddressObject.GState),
                    new CardListLine("Country:", AddressObject.GCountry),

                    new CardListLine("Date:", AddressObject.GDate.ShortDate),
                    new CardListLine("Postal:", AddressObject.GPostal),
                    new CardListLine("Phone:", AddressObject.GPhone),
                });

                // Get date card
                BaseDetail.Add(AddressObject.GDate.AsHLink("Address Date"));

                // Add Standard details
                BaseDetail.Add(DV.AddressDV.GetModelInfoFormatted(AddressObject));

                // Add map card
                BaseDetail.Add(TurnAddressToURLModel());
            }

            return;
        }

        private URLModel TurnAddressToURLModel()
        {
            URLModel mapModel = new URLModel
            {
                GDescription = AddressObject.DefaultText,
                URLType = URIType.Map,
            };

            mapModel.MapLocation.Thoroughfare = AddressObject.GStreet;
            mapModel.MapLocation.Locality = AddressObject.GCity;
            mapModel.MapLocation.AdminArea = AddressObject.GState;
            mapModel.MapLocation.CountryName = AddressObject.GCountry;

            return mapModel;
        }
    }
}