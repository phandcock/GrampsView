// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    /// <summary>
    /// XML 1.71 not part of
    /// </summary>
    public class GrampsStyleRangeModel : ModelBase, IComparable<GrampsStyleRangeModel>, IEquatable<GrampsStyleRangeModel>
    {
        public GrampsStyleRangeModel()
        {
        }

        [DataMember]
        public int End { get; set; }

        [DataMember]
        public int Start { get; set; }

        public int CompareTo(GrampsStyleRangeModel other)
        {
            Contract.Assert(other != null);

            return string.Compare(GetDefaultText, other.GetDefaultText, true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public bool Equals(GrampsStyleRangeModel other)
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
            return Equals(obj as GrampsStyleRangeModel);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}