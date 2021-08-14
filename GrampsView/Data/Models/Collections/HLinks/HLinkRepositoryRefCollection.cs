namespace GrampsView.Data.Collections
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// <para> Store a collection of Repository HLinks. </para>
    /// <para> XML 1.71 check done </para>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkRepositoryRefModel>))]
    public class HLinkRepositoryRefCollection : HLinkBaseCollection<HLinkRepositoryRefModel>
    {
        public HLinkRepositoryRefCollection()
        {
            Title = "Repository Reference Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkRepositoryRefModel argHLink in this)
            {
                ItemGlyph t = DV.RepositoryDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            base.SetGlyph();
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void Sort()
        {
            // Sort the collection
            List<HLinkRepositoryRefModel> t = this.OrderBy(HLinkRepositoryRefModel => HLinkRepositoryRefModel.DeRef.DefaultText).ToList();

            Items.Clear();

            foreach (HLinkRepositoryRefModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}