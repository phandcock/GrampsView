using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Minor;

using System.Threading.Tasks;

namespace GrampsView.Models.HLinks.Models
{
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
            HLinkGlyphItem.Symbol = Constants.IconMap;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the Map model.
        /// </summary>
        /// <value>
        /// The dereference.
        /// </value>

        public IMapModel DeRef
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
    }
}