/// <summary>
/// Collection of HLinks to Address models.
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Collection of HLinks to Address models.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkAdressModel>))]
    public class HLinkAddressModelCollection : HLinkBaseCollection<HLinkAdressModel>
    {
        public HLinkAddressModelCollection()
        {
            Title = "Address Collection";
        }

        //public override CardGroup GetCardGroup()
        //{
        //    CardGroup t = base.GetCardGroup();

        // t.Title = Title;

        //    return t;
        //}

        public override void SetGlyph()
        {
            foreach (HLinkAdressModel argHLink in this)
            {
                ItemGlyph t = DV.AddressDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            base.SetGlyph();
        }

        ///// <summary>
        ///// Helper method to sort
        ///// </summary>
        //public void Sort()
        //{
        //    // Sort the collection
        //    List<HLinkAdressModel> t = this.OrderBy(HLinkAdressModel => HLinkAdressModel.DeRef.DefaultText).ToList();

        // Items.Clear();

        //    foreach (HLinkAdressModel item in t)
        //    {
        //        Items.Add(item);
        //    }
        //}
    }
}