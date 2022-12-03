using GrampsView.Models.DataModels;

using System;
using System.Collections.Generic;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.Data.Model
{
    //// Gramps XML 1.71
    //// style
    //// name
    //// value
    //// range


    /// <summary>
    ///   <br />
    /// </summary>
    public class GrampsStyle : ModelBase, IGrampsStyle, IComparable<GrampsStyle>, IEquatable<GrampsStyle>
    {
        public GrampsStyle()
        {
        }

        public List<GrampsStyleRangeModel> GRange
        {
            get; set;
        }
        = new List<GrampsStyleRangeModel>();

        public TextStyle GStyle
        {
            get; set;
        }
        = TextStyle.unknown;

        public string GValue
        {
            get; set;
        }

        public int CompareTo(GrampsStyle other)
        {
            return other is null
                ? throw new ArgumentNullException(nameof(other))
                : string.Compare(ToString(), other.ToString(), true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public bool Equals(GrampsStyle other)
        {
            if (other is null)
            {
                return false;
            }

            return ToString() == other.ToString();
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