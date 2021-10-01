namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Collection of HLinks to LDS Ordination models.
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

    [KnownType(typeof(ObservableCollection<HLinkLDSModel>))]
    public class HLinkLdsOrdModelCollection : HLinkBaseCollection<HLinkLDSModel>
    {
        public HLinkLdsOrdModelCollection()
        {
            Title = "LDS Ordination Collection";
        }
    }
}