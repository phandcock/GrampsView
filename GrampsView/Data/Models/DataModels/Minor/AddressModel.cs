//-----------------------------------------------------------------------
//
// Handles GRAMPS Address fields
//
// <copyright file="AddressModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;
    using GrampsView.Data.Collections;

    using System.Runtime.Serialization;
    using GrampsView.Common;

    /// <summary>
    /// XML 1.71 all done
    /// </summary>
    public class AddressModel : ModelBase, IAddressModel, IComparable<AddressModel>, IEquatable<AddressModel>
    {
        public AddressModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconAddress;
            HomeImageHLink.HomeSymbolColour = HomeImageHLink.HomeSymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAddress");
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection { get; set; } = new HLinkCitationModelCollection();

        [DataMember]
        public string GCity { get; set; } = string.Empty;

        [DataMember]
        public string GCountry { get; set; } = string.Empty;

        [DataMember]
        public string GCounty { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Date recorded for the address.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [DataMember]
        public DateObjectModel GDate { get; set; } = new DateObjectModelVal();

        /// <summary>
        /// Gets the formatted.
        /// </summary>
        /// <value>
        /// The formatted address.
        /// </value>
        public override string GetDefaultText
        {
            get
            {
                string formattedAddress = string.Empty;

                if (!string.IsNullOrEmpty(GStreet))
                {
                    formattedAddress = formattedAddress + GStreet + ",";
                }

                if (!string.IsNullOrEmpty(GLocality))
                {
                    formattedAddress = formattedAddress + GLocality + ",";
                }

                if (!string.IsNullOrEmpty(GCity))
                {
                    formattedAddress = formattedAddress + GCity + ",";
                }

                if (!string.IsNullOrEmpty(GCounty))
                {
                    formattedAddress = formattedAddress + GCounty + ",";
                }

                if (!string.IsNullOrEmpty(GState))
                {
                    formattedAddress = formattedAddress + GState + ",";
                }

                if (!string.IsNullOrEmpty(GCountry))
                {
                    formattedAddress = formattedAddress + GCountry + ",";
                }

                return formattedAddress;
            }
        }

        /// <summary>
        /// Gets or sets the locality.
        /// </summary>
        /// <value>
        /// The locality.
        /// </value>
        [DataMember]
        public string GLocality { get; set; } = string.Empty;

        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection { get; set; } = new HLinkNoteModelCollection();

        [DataMember]
        public string GPhone { get; set; } = string.Empty;

        [DataMember]
        public string GPostal { get; set; } = string.Empty;

        [DataMember]
        public string GState { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Street name and number.
        /// </summary>
        [DataMember]
        public string GStreet { get; set; } = string.Empty;

        /// <summary>
        /// Gets the hlink for the Address Model.
        /// </summary>
        /// <value>
        /// The h link.
        /// </value>
        public HLinkAdressModel HLink
        {
            get
            {
                HLinkAdressModel t = new HLinkAdressModel
                {
                    HLinkKey = HLinkKey,
                };
                return t;
            }
        }

        public int CompareTo(AddressModel other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return string.Compare( GetDefaultText, other.GetDefaultText,true,System.Globalization.CultureInfo.CurrentCulture);
        }

        public bool Equals(AddressModel other)
        {
            if (other is null)
            {
                return false;
            }

            if (GetDefaultText == other.GetDefaultText)
            {
                return true;
            }

            return false;
        }
    }
}