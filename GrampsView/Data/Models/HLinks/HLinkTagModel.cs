// XML 1.71 All done

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkTagModel : HLinkBase, IHLinkTagModel
    {
        ///// <summary>
        ///// The local image h link.
        ///// </summary>
        //private HLinkMediaModel localImageHLink = new HLinkMediaModel();

        public HLinkTagModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconTag;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the dereferenced Tag ViewModel.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public TagModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.TagDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "TagDetailPage");
            return;
        }
    }
}