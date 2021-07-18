namespace GrampsView.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using static GrampsView.Common.CommonEnums;

    //// Gramps XML 1.71
    //// style
    //// name
    //// value
    //// range
    /// </summary>

    [DataContract]
    public class GrampsStyle : ModelBase, IGrampsStyle, IComparable<GrampsStyle>, IEquatable<GrampsStyle>
    {
        public GrampsStyle()
        {
        }

        [DataMember]
        public List<GrampsStyleRangeModel> GRange
        {
            get; set;
        }
        = new List<GrampsStyleRangeModel>();

        [DataMember]
        public TextStyle GStyle
        {
            get; set;
        }
        = TextStyle.unknown;

        [DataMember]
        public string GValue
        {
            get; set;
        }

        public int CompareTo(GrampsStyle other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return string.Compare(GetDefaultText, other.GetDefaultText, true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public bool Equals(GrampsStyle other)
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

        public override bool Equals(object obj)
        {
            return Equals(obj as GrampsStyle);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}