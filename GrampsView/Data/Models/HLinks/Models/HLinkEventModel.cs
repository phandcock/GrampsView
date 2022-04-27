// XML 171 - All fields defined

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to Event Model but with its own fields as per Gramps
    /// </summary>

    public class HLinkEventModel : HLinkBase, IHLinkEventModel
    {
        private EventModel _Deref = new EventModel();

        private string _GRole;

        private bool DeRefCached = false;

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
        [JsonIgnore]
        public EventModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.EventDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
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

        public HLinkAttributeModelCollection GAttributeRefCollection { get; set; } = new HLinkAttributeModelCollection();

        /// <summary>
        /// Gets or sets the Note Model collection.
        /// </summary>
        /// <value>
        /// The g note model collection.
        /// </value>

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

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}