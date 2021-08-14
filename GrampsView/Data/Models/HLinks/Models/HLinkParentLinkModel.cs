namespace GrampsView.Data.Model
{
    using GrampsView.Common;
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
    /// <description> Not in XML 1.71 so use the base HLink. </description>
    /// </item>
    /// </list>
    /// </summary>

    public class HLinkParentLinkModel : HLinkBase, IHLinkParentLinkModel
    {
        public HLinkParentLinkModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconAttribute;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAttribute");
        }

        /// <summary>
        /// Gets the dereference.
        /// </summary>
        /// <value>
        /// The dereference.
        /// </value>

        public new FamilyModel DeRef
        {
            get;

            set;
        } = new FamilyModel();

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

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this.DeRef.HLink, nameof(FamilyDetailPage));
            return;
        }

        protected override IModelBase GetDeRef()
        {
            return this.DeRef;
        }
    }
}