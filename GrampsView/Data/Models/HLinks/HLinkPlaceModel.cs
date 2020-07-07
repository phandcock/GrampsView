//-----------------------------------------------------------------------
//
// Various note models
//
// <copyright file="HLinkPlaceModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "placeref-content" >
////  < attribute name="hlink">
////    <data type = "IDREF" />
////  </ attribute >
////  < optional >
////    <ref name="date-content" />
////  </optional>
////</define>

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
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