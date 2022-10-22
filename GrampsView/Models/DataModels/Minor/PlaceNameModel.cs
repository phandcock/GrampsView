// TODO Needs XML 1.71 check

using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Date;

using System;

namespace GrampsView.Models.DataModels.Minor
{
    /// <summary>
    /// Gramps XML 1.71 value lang date-content
    /// </summary>
    /// TODO Update fields as per Schema
    public class PlaceNameModel : ModelBase, IPlaceNameModel, IComparable<PlaceNameModel>, IEquatable<PlaceNameModel>
    {
        public PlaceNameModel()
        {
        }

        public DateObjectModel GDate { get; set; } = new DateObjectModelVal();

        public string GLang { get; set; } = string.Empty;

        public string GValue { get; set; } = string.Empty;

        public HLinkPlaceNameModel HLink
        {
            get
            {
                HLinkPlaceNameModel t = new HLinkPlaceNameModel
                {
                    DeRef = this,
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        public int CompareTo(PlaceNameModel other)
        {
            return other is null
                ? throw new ArgumentNullException(nameof(other))
                : string.Compare(ToString(), other.ToString(), true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public bool Equals(PlaceNameModel other)
        {
            if (other is null)
            {
                return false;
            }

            return ToString() == other.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PlaceNameModel);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }

        public override string ToString()
        {
            if (!Valid)
            {
                return string.Empty;
            }

            return !string.IsNullOrEmpty(GValue) ? GValue : GValue;
        }
    }
}