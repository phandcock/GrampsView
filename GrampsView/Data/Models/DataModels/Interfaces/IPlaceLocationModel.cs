namespace GrampsView.Data.Model
{
    /// <summary>
    /// Interfaces for the PlaceLocation model.
    /// </summary>
    public interface IPlaceLocationModel : IModelBase
    {
        string GCity
        {
            get; set;
        }

        string GCountry
        {
            get; set;
        }

        string GCounty
        {
            get; set;
        }

        string GLocality
        {
            get; set;
        }

        string GParish
        {
            get; set;
        }

        string GPhone
        {
            get; set;
        }

        string GPostal
        {
            get; set;
        }

        string GState
        {
            get; set;
        }

        string GStreet
        {
            get; set;
        }
    }
}