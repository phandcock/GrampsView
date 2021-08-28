namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Data model for a place location.
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Done </description>
    /// </item>
    /// </list>
    /// </summary>
    public class PlaceLocationModel : ModelBase, IPlaceLocationModel
    {
        public PlaceLocationModel()
        {
        }

        [DataMember]
        public string GCity
        {
            get; set;
        }

        [DataMember]
        public string GCountry
        {
            get; set;
        }

        [DataMember]
        public string GCounty
        {
            get; set;
        }

        [DataMember]
        public string GLocality
        {
            get; set;
        }

        [DataMember]
        public string GParish
        {
            get; set;
        }

        [DataMember]
        public string GPhone
        {
            get; set;
        }

        [DataMember]
        public string GPostal
        {
            get; set;
        }

        [DataMember]
        public string GState
        {
            get; set;
        }

        [DataMember]
        public string GStreet
        {
            get; set;
        }
    }
}