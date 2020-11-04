// TODO Needs XML 1.71 check

namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// URL model collection.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<URLModel>))]
    public class OCURLModelCollection : CardGroupBase<URLModel>
    {
        public OCURLModelCollection()
        {
            Title = "URL Model Collection";
        }
    }
}