// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.DBModels;
using GrampsView.ModelsDB.HLinks.Models;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GrampsView.ModelsDB.Collections.HLinks
{
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

    [KnownType(typeof(ObservableCollection<HLinkEventDBModel>))]
    public class HLinkEventDBModelCollection : HLinkDBBaseCollection<HLinkEventDBModel>
    {
        public HLinkEventDBModelCollection()
        {
            Title = "Event Collection";
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The dereference.
        /// </value>
        [JsonIgnore]
        public ObservableCollection<EventDBModel> DeRef
        {
            get
            {
                ObservableCollection<EventDBModel> t = new ObservableCollection<EventDBModel>();

                foreach (HLinkEventDBModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        public override void SetGlyph()
        {
            foreach (HLinkEventDBModel argHLink in this)
            {
                ItemGlyph t = DL.EventDL.GetGlyph(argHLink.HLinkKey);

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
            List<HLinkEventDBModel> t = this.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GDate.SortDate).ToList();

            Items.Clear();

            foreach (HLinkEventDBModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}