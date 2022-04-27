namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Data model for a Citation HLink.
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

    public class HLinkCitationModel : HLinkBase, IHLinkCitationModel
    {
        private CitationModel _Deref = new CitationModel();

        private bool DeRefCached = false;

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
        [JsonIgnore]
        public CitationModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.CitationDV.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        /// <summary>
        /// CompareTo. Bases it on the HLinkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(object obj)
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

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}