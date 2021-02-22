// XML 171 - All fields defined

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// HLink to the Family Model.
    /// </summary>
    [DataContract]
    public class HLinkFamilyModel : HLinkBase, IHLinkFamilyModel
    {
        public HLinkFamilyModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconFamilies;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundFamily");
        }

        /// <summary>
        /// Gets.
        /// </summary>
        public FamilyModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return new FamilyDataView().GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new FamilyModel();
                }
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(FamilyDetailPage));
            return;
        }
    }
}