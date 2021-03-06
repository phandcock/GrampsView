﻿// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkMediaModel : HLinkBase, IHLinkMediaModel
    {
        private MediaModel _Deref = new MediaModel();

        private bool DeRefCached = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="HLinkMediaModel"/> class.
        /// </summary>
        public HLinkMediaModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconMedia;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundMedia");
        }

        /// <summary>
        /// Gets the associated media model
        /// </summary>
        /// <value>
        /// The media model. <note type="caution">This can not hold a local copy of the media model
        /// as the Model Base has a hlinkmediamodel in it and this will cause a referene loop</note>
        /// </value>
        public new IMediaModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.MediaDV.GetModelFromHLinkKey(HLinkKey);
                    DeRefCached = true;
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Gets or sets the Attribute.
        /// </summary>
        /// <value>
        /// The Attribute.
        /// </value>
        [DataMember]
        public HLinkAttributeModelCollection GAttributeRefCollection { get; set; } = new HLinkAttributeModelCollection();

        /// <summary>
        /// Gets or sets the g citation model collection.
        /// </summary>
        /// <value>
        /// The g citation model collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection { get; set; } = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the g note model collection.
        /// </summary>
        /// <value>
        /// The g note model collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection { get; set; } = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid. <note
        /// type="note">Can have a HLink or be a pointer to an image. <br/><br/> So, MUST be valid
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