// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkNameMapModel : HLinkBase, IHLinkNameMapModel
    {
        public HLinkNameMapModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconNameMaps;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNameMap");
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public NameMapModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new NameMapDataView().GetModelFromHLinkKey(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "NameMapDetailPage");
            return;
        }
    }
}