// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Models.Collections.HLinks;
    using GrampsView.ModelsDB.Collections.HLinks;

    /// <summary>
    /// Data model for a person reference.
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

    public sealed class HLinkPersonRefModel : HLinkPersonModel
    {
        public HLinkPersonRefModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconPeople;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        public HLinkCitationDBModelCollection GCitationCollection
        {
            get; set;
        }

          = new HLinkCitationDBModelCollection();

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>

        public HLinkNoteDBModelCollection GNoteCollection
        {
            get; set;
        }

            = new HLinkNoteDBModelCollection();

        public string GRelationship
        {
            get; set;
        }
    }
}