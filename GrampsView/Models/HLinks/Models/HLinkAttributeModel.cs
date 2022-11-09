namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Models.HLinks;
    using GrampsView.Views;

    using System.Threading.Tasks;

    /// <summary>
    /// <para> HLink to an Attribute model. </para>
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// </item>
    /// </list>
    /// </summary>

    public class HLinkAttributeModel : HLinkBase, IHLinkAttributeModel
    {
        public HLinkAttributeModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconAttribute;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAttribute");
            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The dereference.
        /// </value>

        public AttributeModel DeRef
        {
            get;

            set;
        } = new AttributeModel();

        public override bool Valid
        {
            get
            {
                return HLinkGlyphItem.Valid;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(AttributeDetailPage));
            return;
        }
    }
}