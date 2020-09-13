namespace GrampsView.Data.Model
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Gramps XML 1.71 value lang date-content
    /// </summary>
    /// TODO Update fields as per Schema
    public class PlaceNameModel : ModelBase, IPlaceNameModel, IComparable<PlaceNameModel>, IEquatable<PlaceNameModel>
    {
        public PlaceNameModel()
        {
        }

        [DataMember]
        public IDateObjectModel GDate { get; set; } = new DateObjectModelVal();

        [DataMember]
        public string GLang { get; set; }

        [DataMember]
        public string GValue { get; set; }

        public int CompareTo(PlaceNameModel other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return string.Compare(GetDefaultText, other.GetDefaultText, true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public bool Equals(PlaceNameModel other)
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