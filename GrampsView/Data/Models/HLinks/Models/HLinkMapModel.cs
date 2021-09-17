namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Data model for a Map Reference.
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> NA </description>
    /// </item>
    /// </list>
    /// </summary>

    public class HLinkMapModel : HLinkBase, IHLinkMapModel
    {
        public HLinkMapModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconMap;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the Map model.
        /// </summary>
        /// <value>
        /// The dereference.
        /// </value>

        [JsonIgnore]
        public new IMapModel DeRef
        {
            get;

            set;
        } = new MapModel();

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
        /// No detail page to navigate to, just open the Map externally.
        /// </summary>
        public override async Task UCNavigate()
        {
            await DeRef.OpenMap();
            return;
        }

        //protected override IModelBase GetDeRef()
        //{
        //    return this.DeRef;
        //}
    }
}