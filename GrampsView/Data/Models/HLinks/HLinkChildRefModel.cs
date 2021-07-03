namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// TODO xml 1.71 needed Child Reference model
    /// </summary>
    [DataContract]
    public class HLinkChildRefModel : HLinkBase, IHLinkChildRefModel
    {
        private PersonModel _Deref = new PersonModel();

        private bool DeRefCached = false;

        public HLinkChildRefModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconPeople;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        public new PersonModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.PersonDV.GetModelFromHLinkKey(HLinkKey);
                    DeRefCached = true;
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Citation collection reference.
        /// </summary>
        /// <value>
        /// The citation collection reference.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationCollectionReference
        {
            get; set;
        }

        = new HLinkCitationModelCollection();

        [DataMember]
        public string GFatherRel
        {
            get;
            set;
        }

        = string.Empty;

        [DataMember]
        public string GMotherRel
        {
            get;
            set;
        }

        = string.Empty;

        /// <summary>
        /// Gets the note collection reference.
        /// </summary>
        /// <value>
        /// The note collection reference.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteCollectionReference
        {
            get; set;
        }

        = new HLinkNoteModelCollection();

        public string RelationShips
        {
            get
            {
                if (!string.IsNullOrEmpty(GFatherRel) & !string.IsNullOrEmpty(GMotherRel))
                {
                    return $"{GFatherRel}-{GMotherRel}";
                }

                return string.Empty;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(ChildRefDetailPage));
            return;
        }
    }
}