namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Threading.Tasks;

    /// <summary>
    /// Child Reference model
    /// </summary>
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

        public string GGFRel
        {
            get;
            set;
        }

        = string.Empty;

        public string GGMRel
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
        public HLinkNoteModelCollection GNoteCollectionReference
        {
            get; set;
        }

        = new HLinkNoteModelCollection();

        public string RelationShips
        {
            get
            {
                if (!string.IsNullOrEmpty(GGFRel) & !string.IsNullOrEmpty(GGMRel))
                {
                    return string.Format("{0}-{1}", GGFRel, GGMRel);
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