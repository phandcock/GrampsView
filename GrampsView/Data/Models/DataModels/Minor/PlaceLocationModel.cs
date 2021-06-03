// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System;
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
        public String GLocationName
        {
            get; set;
        }

        [DataMember]
        public CommonEnums.PlaceLocation GPlaceLocation { get; set; } = CommonEnums.PlaceLocation.country;

        //public string GPlaceLocationDecoded
        //{
        //    get
        //    {
        //        switch (GPlaceLocation)
        //        {
        //            case CommonEnums.PlaceLocation.city:
        //                {
        //                    return "city";
        //                }
        //            case CommonEnums.PlaceLocation.country:
        //                {
        //                    return "country";
        //                }
        //            case CommonEnums.PlaceLocation.county:
        //                {
        //                    return "county";
        //                }
        //            case CommonEnums.PlaceLocation.locality:
        //                {
        //                    return "locality";
        //                }
        //            case CommonEnums.PlaceLocation.parish:
        //                {
        //                    return "parish";
        //                }
        //            case CommonEnums.PlaceLocation.phone:
        //                {
        //                    return "phone";
        //                }
        //            case CommonEnums.PlaceLocation.postal:
        //                {
        //                    return "postal";
        //                }
        //            case CommonEnums.PlaceLocation.state:
        //                {
        //                    return "state";
        //                }
        //            case CommonEnums.PlaceLocation.street:
        //                {
        //                    return "street";
        //                }
        //            default:
        //                {
        //                    return "unknown place location? " + GPlaceLocation;
        //                }
        //        }
        //    }
        //}
    }
}