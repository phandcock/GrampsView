// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    using Xamarin.Forms;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkPlaceModel : HLinkBase, IHLinkPlaceModel
    {
        public HLinkPlaceModel()
        {
            UCNavigateCommand = new Command<HLinkPlaceModel>(UCNavigate);
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

        public async void UCNavigate(HLinkPlaceModel argHLink)
        {
            await UCNavigateBase(argHLink, "PlaceDetailPage");
        }
    }
}