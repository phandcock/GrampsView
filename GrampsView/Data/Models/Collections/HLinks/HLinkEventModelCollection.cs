namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Collection of Event HLinks
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
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkEventModel>))]
    public class HLinkEventModelCollection : HLinkBaseCollection<HLinkEventModel>
    {
        public HLinkEventModelCollection()
        {
            Title = "Event Collection";
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The dereference.
        /// </value>
        public ObservableCollection<EventModel> DeRef
        {
            get
            {
                ObservableCollection<EventModel> t = new ObservableCollection<EventModel>();

                foreach (HLinkEventModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        //public override CardGroup GetCardGroup()
        //{
        //    CardGroup t = base.GetCardGroup();

        // t.Title = Title;

        //    return t;
        //}

        public override void SetGlyph()
        {
            foreach (HLinkEventModel argHLink in this)
            {
                ItemGlyph t = DV.EventDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            SetFirstImage();

            if (CommonLocalSettings.SortHLinkCollections)
            {
                Sort();
            }
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void Sort()
        {
            // Sort the collection
            List<HLinkEventModel> t = this.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GDate.SortDate).ToList();

            Items.Clear();

            foreach (HLinkEventModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}