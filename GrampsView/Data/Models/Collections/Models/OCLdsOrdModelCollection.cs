// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// LDS Ordination model collection
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<LdsOrdModel>))]
    public class OCLdsOrdModelCollection : CardGroupBase<LdsOrdModel>
    {
        public OCLdsOrdModelCollection()
        {
            Title = "LDS Ordination Collection";
        }
    }
}