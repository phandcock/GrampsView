/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Source Attribute model collection
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.SrcAttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<SrcAttributeModel>))]
    public class OCSrcAttributeModelCollection : CardGroupBase<SrcAttributeModel>
    {
        public OCSrcAttributeModelCollection()
        {
            Title = "Source Attribute Collection";
        }
    }
}