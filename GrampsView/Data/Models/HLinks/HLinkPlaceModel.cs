// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkPlaceModel : HLinkBase, IHLinkPlaceModel
    {
        public HLinkPlaceModel()
        {
        }

        public PlaceModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.PlaceDV.GetModelFromHLinkString(HLinkKey);
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