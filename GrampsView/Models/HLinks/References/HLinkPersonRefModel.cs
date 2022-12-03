namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Models.Collections.HLinks;

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

        public HLinkCitationModelCollection GCitationCollection
        {
            get; set;
        }

          = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>

        public HLinkNoteModelCollection GNoteCollection
        {
            get; set;
        }

            = new HLinkNoteModelCollection();

        public string GRelationship
        {
            get; set;
        }
    }
}