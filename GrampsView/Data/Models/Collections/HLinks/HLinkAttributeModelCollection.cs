// TODO Needs XML 1.71 check

namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// URL model collection.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkAttributeModel>))]
    public class HLinkAttributeModelCollection : HLinkBaseCollection<HLinkAttributeModel>
    {
        public HLinkAttributeModelCollection()
        {
            Title = "Attribute Model Collection";
        }
    }
}