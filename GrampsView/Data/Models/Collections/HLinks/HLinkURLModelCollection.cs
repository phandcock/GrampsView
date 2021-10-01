namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// URL HLink collection
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
    /// <para> <br/> </para>
    /// </summary>

    [KnownType(typeof(ObservableCollection<HLinkURLModel>))]
    public class HLinkURLModelCollection : HLinkBaseCollection<HLinkURLModel>
    {
        public HLinkURLModelCollection()
        {
            Title = "URL Model Collection";
        }
    }
}