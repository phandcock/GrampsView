namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    ///
    /// XML 1.71 All done
    /// </summary>

    [DataContract]
    public class HLinkTagModel : HLinkBase, IHLinkTagModel
    {
        private TagModel _Deref = new TagModel();

        private bool DeRefCached = false;

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
        public new TagModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
                {
                    _Deref = DV.TagDV.GetModelFromHLinkKey(HLinkKey);
                    DeRefCached = true;
                }

                return _Deref;
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "TagDetailPage");
            return;
        }
    }
}