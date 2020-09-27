using GrampsView.Common;

namespace GrampsView.Data.Model
{
    public interface IPlaceLocationModel : IModelBase
    {
        string GLocationName { get; set; }

        CommonEnums.PlaceLocation GPlaceLocation { get; set; }

        string GPlaceLocationDecoded { get; }
    }
}