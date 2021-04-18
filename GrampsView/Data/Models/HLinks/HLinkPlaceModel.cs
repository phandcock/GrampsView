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
    public class HLinkPlaceModel : HLinkBase, IHLinkPlaceModel
    {
        public HLinkPlaceModel()
        {
            HLinkGlyphItem.Symbol = CommonConstants.IconPlace;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPlace");
        }

        public PlaceModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.PlaceDV.GetModelFromHLinkKey(HLinkKey);
                }
                else
                {
                    return null;
                }
            }
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, "PlaceDetailPage");
            return;
        }
    }
}