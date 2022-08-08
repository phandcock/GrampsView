namespace GrampsView.Data.Model
{
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

        public string GCity
        {
            get; set;
        }

        public string GCountry
        {
            get; set;
        }

        public string GCounty
        {
            get; set;
        }

        public string GLocality
        {
            get; set;
        }

        public string GParish
        {
            get; set;
        }

        public string GPhone
        {
            get; set;
        }

        public string GPostal
        {
            get; set;
        }

        public string GState
        {
            get; set;
        }

        public string GStreet
        {
            get; set;
        }
    }
}