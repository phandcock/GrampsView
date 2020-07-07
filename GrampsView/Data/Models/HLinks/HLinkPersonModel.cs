//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkPersonModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "personref-content" >
////  < attribute name="hlink">
////    <data type = "IDREF" />
////  </ attribute >
////  < optional >
////    < attribute name="priv">
////      <ref name="priv-content" />
////    </attribute>
////  </optional>
////  <attribute name = "rel" >
////    < text />
////  </ attribute >
////  < optional >
////    < zeroOrMore >
////      < element name="citationref">
////        <ref name="citationref-content" />
////      </element>
////    </zeroOrMore>
////  </optional>
////  <optional>
////    <zeroOrMore>
////      <element name = "noteref" >
////        <ref name="noteref-content" />
////      </element>
////    </zeroOrMore>
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
    public sealed class HLinkPersonModel : HLinkBase, IHLinkPersonModel
    {
        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public PersonModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.PersonDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new PersonModel();
                }
            }
        }
    }
}