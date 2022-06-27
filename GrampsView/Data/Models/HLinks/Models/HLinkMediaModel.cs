namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink for a media object.
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

    public class HLinkMediaModel : HLinkBase, IHLinkMediaModel
    {
        private MediaModel _Deref = new MediaModel();

        private bool DeRefCached = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="HLinkMediaModel"/> class.
        /// </summary>
        public HLinkMediaModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconMedia;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundMedia");
        }

        /// <summary>
        /// Gets the associated media model
        /// </summary>
        /// <value>
        /// The media model.
        /// </value>
        [JsonIgnore]
        public IMediaModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.MediaDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Gets or sets the Attribute collection.
        /// </summary>
        /// <value>
        /// The Attribute.
        /// </value>

        public HLinkAttributeModelCollection GAttributeRefCollection { get; set; } = new HLinkAttributeModelCollection();

        /// <summary>
        /// Gets or sets the citation model collection.
        /// </summary>
        /// <value>
        /// The citation model collection.
        /// </value>

        public HLinkCitationModelCollection GCitationRefCollection { get; set; } = new HLinkCitationModelCollection();

        public int GCorner1X
        {
            get; set;
        }

        public int GCorner1Y
        {
            get; set;
        }

        public int GCorner2X
        {
            get; set;
        }

        public int GCorner2Y
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the note model collection.
        /// </summary>
        /// <value>
        /// The g note model collection.
        /// </value>

        public HLinkNoteModelCollection GNoteRefCollection { get; set; } = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid. <note
        /// type="note"> Can have a HLink or be a pointer to an image. <br/><br/> So, MUST be valid
        /// for both types and MUST be invalid for a default new instance. <br/></note>
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        public override bool Valid
        {
            get
            {
                return HLinkKey.Valid;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "MediaDetailPage");
            return;
        }
    }
}