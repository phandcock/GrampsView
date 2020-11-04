// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    /// <summary>
    /// XML 1.71 not part of
    /// </summary>
    public class StyledTextRangeModel : ModelBase, IComparable<StyledTextRangeModel>, IEquatable<StyledTextRangeModel>
    {
        public StyledTextRangeModel()
        {
        }

        [DataMember]
        public int End { get; set; }

        [DataMember]
        public int Start { get; set; }

        public int CompareTo(StyledTextRangeModel other)
        {
            Contract.Assert(other != null);

            return string.Compare(GetDefaultText, other.GetDefaultText, true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public bool Equals(StyledTextRangeModel other)
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
            return Equals(obj as StyledTextRangeModel);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}