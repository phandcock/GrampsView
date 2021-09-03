/// <summary>
/// Collection of HLinks to Address models.
/// </summary>
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

        /// <summary>
        /// Sort by address date rather than the default text
        /// </summary>
        public override void Sort()
        {
            List<HLinkAdressModel> t = this.OrderBy(HLinkAdressModel => HLinkAdressModel.DeRef.GDate).ToList();

            Items.Clear();

            foreach (HLinkAdressModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}