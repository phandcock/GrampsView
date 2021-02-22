// XML 171 - All fields defined

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to Event Model but with its own fields as per Gramps
    /// </summary>
    [DataContract]
    public class HLinkEventModel : HLinkBase, IHLinkEventModel
    {
        private EventModel _Deref = new EventModel();

        [DataMember]
        private string _GRole;

        public HLinkEventModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconEvents;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundEvent");
        }

        /// <summary>
        /// Gets the Event Model pointed to.
        /// </summary>
        /// <value>
        /// The Event Model.
        /// </value>
        public EventModel DeRef
        {
            get
            {
                if (Valid & (!_Deref.Valid))
                {
                    _Deref = DV.EventDV.GetModelFromHLinkString(HLinkKey);
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Gets or sets the Attribute Collection.
        /// </summary>
        /// <value>
        /// The Attribute.
        /// </value>
        [DataMember]
        public OCAttributeModelCollection GAttributeRefCollection { get; set; } = new OCAttributeModelCollection();

        /// <summary>
        /// Gets or sets the Note Model collection.
        /// </summary>
        /// <value>
        /// The g note model collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection { get; set; } = new HLinkNoteModelCollection();

        public string GRole
        {
            get
            {
                return _GRole;
            }

            set
            {
                SetProperty(ref _GRole, value);
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "EventDetailPage");
            return;
        }
    }
}