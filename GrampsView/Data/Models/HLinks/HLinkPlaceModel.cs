﻿// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkPlaceModel : HLinkBase, IHLinkPlaceModel
    {
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
    }
}