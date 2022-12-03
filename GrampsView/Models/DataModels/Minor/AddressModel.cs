using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels.Date;

using System;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.Models.DataModels.Minor
{
    /// <summary>
    /// XML 1.71 all done
    /// </summary>
    public class AddressModel : ModelBase, IAddressModel, IComparable<AddressModel>, IEquatable<AddressModel>
    {
        public AddressModel()
        {
            ModelItemGlyph.Symbol = Constants.IconAddress;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAddress");
        }

        /// <summary>
        /// Gets or sets the citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>

        public HLinkCitationModelCollection GCitationRefCollection { get; set; } = new HLinkCitationModelCollection();

        public string GCity { get; set; } = string.Empty;

        public string GCountry { get; set; } = string.Empty;

        public string GCounty { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Date recorded for the address.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>

        public DateObjectModel GDate { get; set; } = new DateObjectModelVal();

        /// <summary>
        /// Gets or sets the locality.
        /// </summary>
        /// <value>
        /// The locality.
        /// </value>

        public string GLocality { get; set; } = string.Empty;

        public HLinkNoteModelCollection GNoteRefCollection { get; set; } = new HLinkNoteModelCollection();

        public string GPhone { get; set; } = string.Empty;

        public string GPostal { get; set; } = string.Empty;

        public string GState { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Street name and number.
        /// </summary>

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
                    HLinkGlyphItem = ModelItemGlyph,
                };
                return t;
            }
        }

        public int CompareTo(AddressModel other)
        {
            return other is null
                ? throw new ArgumentNullException(nameof(other))
                : string.Compare(ToString(), other.ToString(), true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            AddressModel tempObj = obj as AddressModel;

            return ToString() == tempObj.ToString();
        }

        public bool Equals(AddressModel other)
        {
            if (other is null)
            {
                return false;
            }

            return ToString() == other.ToString();
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }

        public IMapModel ToMapModel()
        {
            IMapModel mapModel = new MapModel
            {
                Description = ToString(),
                MapType = MapType.Place,
            };

            mapModel.MyPlaceMark.Thoroughfare = GStreet;
            mapModel.MyPlaceMark.Locality = GCity;
            mapModel.MyPlaceMark.AdminArea = GState;
            mapModel.MyPlaceMark.CountryName = GCountry;

            return mapModel;
        }

        /// <summary>
        /// Gets the formatted.
        /// </summary>
        /// <value>
        /// The formatted address.
        /// </value>
        public override string ToString()
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
}