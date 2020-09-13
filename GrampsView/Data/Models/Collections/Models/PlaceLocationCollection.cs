/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Place location model collection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.AttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<PlaceLocation>))]
    public class PlaceLocationCollection : CardGroupBase<PlaceLocation>
    {
        public PlaceLocationCollection()
        {
            Title = "Place Location Collection";
        }
    }
}