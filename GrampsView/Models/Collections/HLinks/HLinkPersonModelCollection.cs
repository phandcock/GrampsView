using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;

using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace GrampsView.Models.Collections.HLinks
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <remarks>TODO XML 1.71 check needed</remarks>
    /// TODO Edit XML Comment Template for HLinkPersonModelCollection
    public class HLinkPersonModelCollection : HLinkBaseCollection<HLinkPersonModel>
    {
        public HLinkPersonModelCollection()
        {
            Title = "People Collection";
        }

        public HLinkPersonModelCollection(string argTitle)
        {
            Title = argTitle;
        }

        /// <summary>
        /// Gets the dereferenced person models.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        [JsonIgnore]
        public ObservableCollection<PersonModel> DeRef
        {
            get
            {
                ObservableCollection<PersonModel> t = new();

                foreach (HLinkPersonModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        public override void SetGlyph()
        {
            foreach (HLinkPersonModel argHLink in this)
            {
                ItemGlyph t = DV.PersonDV.GetGlyph(argHLink.HLinkKey);

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
        public override void Sort()
        {
            // Sort the collection
            List<HLinkPersonModel> t = this.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef).ToList();

            Items.Clear();

            foreach (HLinkPersonModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}