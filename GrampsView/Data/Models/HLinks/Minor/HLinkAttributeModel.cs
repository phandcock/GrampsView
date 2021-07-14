// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkAttributeModel : HLinkBase, IHLinkAttributeModel
    {
        public HLinkAttributeModel()
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
        [DataMember]
        public new AttributeModel DeRef
        {
            get;

            set;
        } = new AttributeModel();

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
            await UCNavigateBase(this, nameof(AttributeDetailPage));
            return;
        }
    }
}