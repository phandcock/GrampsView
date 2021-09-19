// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using System;

    /// <summary>
    /// Gramps XML 1.71 value lang date-content
    /// </summary>
    /// TODO Update fields as per Schema
    public class PlaceNameModel : ModelBase, IPlaceNameModel, IComparable<PlaceNameModel>, IEquatable<PlaceNameModel>
    {
        public PlaceNameModel()
        {
        }

        public DateObjectModel GDate { get; set; } = new DateObjectModel();

        public string GLang { get; set; } = string.Empty;

        public string GValue { get; set; } = string.Empty;

        public HLinkPlaceNameModel HLink
        {
            get
            {
                HLinkPlaceNameModel t = new HLinkPlaceNameModel
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        public int CompareTo(PlaceNameModel other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return string.Compare(ToString(), other.ToString(), true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public bool Equals(PlaceNameModel other)
        {
            if (other is null)
            {
                return false;
            }

            if (ToString() == other.ToString())
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PlaceNameModel);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}