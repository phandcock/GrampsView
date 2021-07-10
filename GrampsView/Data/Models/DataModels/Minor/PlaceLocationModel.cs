// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Runtime.Serialization;

    /// <summary>
    /// Gramps XML 1.71 value lang date-content
    /// </summary>
    /// TODO Update fields as per Schema
    public class PlaceLocationModel : ModelBase, IPlaceLocationModel
    {
        public PlaceLocationModel()
        {
        }

        [DataMember]
        public string GLocationName
        {
            get; set;
        }

        [DataMember]
        public CommonEnums.PlaceLocation GPlaceLocation { get; set; } = CommonEnums.PlaceLocation.country;
    }
}