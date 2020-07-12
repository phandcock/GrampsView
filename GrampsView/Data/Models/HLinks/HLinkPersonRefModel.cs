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
    using GrampsView.Data.Collections;

    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public sealed class HLinkPersonRefModel : HLinkPersonModel
    {
        [DataMember]
        public HLinkCitationModelCollection GCitationCollection
        {
            get; set;
        }

          = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the g text.
        /// </summary>
        /// <value>
        /// The g text.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteCollection
        {
            get; set;
        }

            = new HLinkNoteModelCollection();

        [DataMember]
        public string GRelationship { get; set; }
    }
}