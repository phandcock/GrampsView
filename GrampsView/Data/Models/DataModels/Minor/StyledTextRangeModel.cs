
namespace GrampsView.Data.Model
{
    using System;

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
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

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
    }
}