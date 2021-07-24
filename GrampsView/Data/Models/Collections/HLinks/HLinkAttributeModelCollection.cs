namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// HLink Attribute model collection.
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Not Done </description>
    /// </item>
    /// </list>
    /// <para> <br/> </para>
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