namespace GrampsView.Data.Collections
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// <para> Store a collection of Repository HLinks. </para>
    /// <para> XML 1.71 check done </para>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkCitationModel>))]
    public class HLinkRepositoryModelCollection : HLinkBaseCollection<HLinkRepositoryModel>
    {
        public HLinkRepositoryModelCollection()
        {
            Title = "Repository Collection";
        }

        public override void SetGlyph()
        {
            foreach (HLinkRepositoryModel argHLink in this)
            {
                ItemGlyph t = DV.RepositoryDV.GetGlyph(argHLink.HLinkKey);

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
        //    List<HLinkRepositoryModel> t = this.OrderBy(HLinkRepositoryModel => HLinkRepositoryModel.DeRef.ToString()).ToList();

        // Items.Clear();

        //    foreach (HLinkRepositoryModel item in t)
        //    {
        //        Items.Add(item);
        //    }
        //}
    }
}