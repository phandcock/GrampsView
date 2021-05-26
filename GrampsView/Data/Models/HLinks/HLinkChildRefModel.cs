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

        public HLinkChildRefModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconPeople;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        public PersonModel DeRef
        {
            get
            {
                if (Valid & (!_Deref.Valid))
                {
                    _Deref = DV.PersonDV.GetModelFromHLinkKey(HLinkKey);
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

        public HLinkPersonModel GetHLinkPerson
        {
            get
            {
                if (Valid)
                {
                    return DV.PersonDV.GetModelFromHLinkKey(HLinkKey).HLink;
                }

                return new HLinkPersonModel();
            }
        }

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
                    return string.Format("{0}-{1}", GFatherRel, GMotherRel);
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