// XML 171 - All fields defined

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to Citation Model.
    /// </summary>
    [DataContract]
    public class HLinkCitationModel : HLinkBase, IHLinkCitationModel
    {
        private CitationModel _Deref = new CitationModel();

        public HLinkCitationModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconCitation;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundCitation");
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public CitationModel DeRef
        {
            get
            {
                if (Valid & (!_Deref.Valid))
                {
                    _Deref = DV.CitationDV.GetModelFromHLinkString(HLinkKey);
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Compares to. Bases it on the HLInkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object obj)
        {
            // Null objects go first
            if (obj is null)
            {
                return 1;
            }

            // Can only compare if they are the same type so assume equal
            if (obj.GetType() != typeof(HLinkCitationModel))
            {
                return 0;
            }

            return DeRef.CompareTo((obj as HLinkCitationModel).DeRef);
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "CitationDetailPage");
            return;
        }
    }
}