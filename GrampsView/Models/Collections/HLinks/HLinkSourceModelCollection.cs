// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Data model for a source reference collection.
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> NA </description>
    /// </item>
    /// </list>
    /// </summary>

    [KnownType(typeof(ObservableCollection<HLinkSourceModel>))]
    public class HLinkSourceModelCollection : HLinkBaseCollection<HLinkSourceModel>
    {
        public HLinkSourceModelCollection()
        {
            Title = "Source Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkSourceModel argHLink in this)
            {
                ItemGlyph t = DV.SourceDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            base.SetGlyph();
        }

        ///// <summary>
        ///// Helper method to sort and set the firt image link.
        ///// </summary>
        //public void Sort()
        //{
        //    // Sort the collection
        //    List<HLinkSourceModel> t = this.OrderBy(HLinkSourceModel => HLinkSourceModel.DeRef.ToString()).ToList();

        // Items.Clear();

        //    foreach (HLinkSourceModel item in t)
        //    {
        //        Items.Add(item);
        //    }
        //}
    }
}