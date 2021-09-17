// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Runtime.Serialization;

    /// <summary>
    /// Place location model collection
    /// </summary>

    [KnownType(typeof(CardGroupBase<PlaceLocationModel>))]
    public class PlaceLocationCollection : CardGroupBase<PlaceLocationModel>
    {
        public PlaceLocationCollection()
        {
            Title = "Place Location Collection";
        }
    }
}