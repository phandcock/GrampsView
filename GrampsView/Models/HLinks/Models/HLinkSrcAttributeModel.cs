namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Models.HLinks;

    using System.Threading.Tasks;

    /// <summary>
    /// HLink to a URL
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
    /// <para> <br/> </para>
    /// </summary>

    public class HLinkSrcAttributeModel : HLinkBase, IHLinkSrcAttributeModel
    {
        public HLinkSrcAttributeModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconDDefault;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>

        public SrcAttributeModel DeRef
        {
            get;

            set;
        } = new SrcAttributeModel();

        public override bool Valid
        {
            get
            {
                if (!HLinkGlyphItem.Valid)
                {
                }

                return HLinkGlyphItem.Valid;
            }
        }

        /// <summary>
        /// No detail page to navigate to, just open the URL externally.
        /// </summary>
        public override async Task UCNavigate()
        {
            // TODO Needed? await DeRef.OpenURL();
            return;
        }
    }
}