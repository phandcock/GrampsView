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

    [KnownType(typeof(CardGroupModel<PlaceLocationModel>))]
    public class PlaceLocationCollection : CardGroupModel<PlaceLocationModel>
    {
        public PlaceLocationCollection()
        {
            Title = "Place Location Collection";
        }
    }
}