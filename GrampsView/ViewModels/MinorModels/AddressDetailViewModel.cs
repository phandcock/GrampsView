// <copyright file="AddressDetailViewModel.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// ViewModel for the Address Detail page.
    /// </summary>
    public class AddressDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// The current address.
        /// </summary>
        private AddressModel _AddressObject = new AddressModel();

        private HLinkMediaModel _MediaCard = new HLinkMediaModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The common logging service.
        /// </param>
        public AddressDetailViewModel(ICommonLogging iocCommonLogging)
            : base(iocCommonLogging)
        {
            BaseTitle = "Address Detail";
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
            get
            {
                return _AddressObject;
            }

            set
            {
                SetProperty(ref _AddressObject, value);
            }
        }

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
        /// </returns>
        public override void BaseHandleAppearingEvent()
        {
            BaseCL.RoutineEntry("AddressDetailViewModel");

            HLinkAdressModel HLinkObject = CommonRoutines.DeserialiseObject<HLinkAdressModel>(Uri.UnescapeDataString(BaseParamsHLink));

            AddressObject = HLinkObject.DeRef;

            if (AddressObject.Valid)
            {
                // Trigger refresh of View fields via INotifyPropertyChanged
                RaisePropertyChanged(string.Empty);

                BaseTitle = AddressObject.GetDefaultText;

                // Get media image
                MediaCard = AddressObject.ModelItemGlyph.HLinkMedia;

                // Get Header Details
                //CardGroup headerCardGroup = new CardGroup { };

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
                BaseDetail.Add(AddressObject.GDate.AsCardListLine());

                // Add Standard details
                BaseDetail.Add(DV.PersonDV.GetModelInfoFormatted(AddressObject));

                // Add map card
                BaseDetail.Add(TurnAddressToURLModel());
            }

            return;
        }

        private URLModel TurnAddressToURLModel()
        {
            URLModel mapModel = new URLModel
            {
                GDescription = AddressObject.GetDefaultText,
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