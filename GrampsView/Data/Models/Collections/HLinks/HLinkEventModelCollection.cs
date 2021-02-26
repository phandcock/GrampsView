// TODO Needs XML 1.71 check

namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Collection of EVent $$(HLinks)$$.
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
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
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

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public override void SetGlyph()
        {
            foreach (HLinkEventModel argHLink in this)
            {
                ItemGlyph t = DV.EventDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.HLinkMediHLink = t.HLinkMediHLink;
            }

            SortAndSetFirst();
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void SortAndSetFirst()
        {
            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.
            EventModel tempModel = new EventModel();

            FirstHLinkHomeImage.ImageType = CommonEnums.HLinkGlyphType.Symbol;

            if (Count > 0)
            {
                // Step through each eventmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempModel = DV.EventDV.EventData.GetModelFromHLink(this[i]);

                    if (tempModel.ModelItemGlyph.ImageType == CommonEnums.HLinkGlyphType.Image)
                    {
                        FirstHLinkHomeImage = tempModel.ModelItemGlyph;
                        break;
                    }
                }

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
}