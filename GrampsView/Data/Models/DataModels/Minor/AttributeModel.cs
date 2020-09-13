//-----------------------------------------------------------------------
//
// Handles GRAMPS Alt fields
//
// <copyright file="AttributeModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// TODO Update fields as per Schema
////<define name = "attribute-content" >
////  < optional >
////    < attribute name="priv">
////      <ref name="priv-content" />
////    </attribute>
////  </optional>
////  <attribute name = "type" >
////    < text />
////  </ attribute >
////  < attribute name="value">
////    <text />
////  </attribute>
////  <zeroOrMore>
////    <element name = "citationref" >
////      <ref name="citationref-content" />
////    </element>
////  </zeroOrMore>
////  <zeroOrMore>
////    <element name = "noteref" >
////      <ref name="noteref-content" />
////    </element>
////  </zeroOrMore>
////</define>
///TODO Update fields as per Schema

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    [DataContract]
    public class AttributeModel : ModelBase, IAttributeModel, IComparable, IComparer<AttributeModel>
    {
        public AttributeModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconAttribute;
            HomeImageHLink.HomeSymbolColour = HomeImageHLink.HomeSymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAttribute");
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationReferenceCollection
        {
            get;
            set;
        }

            = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the g note model reference collection.
        /// </summary>
        /// <value>
        /// The g note model reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteModelReferenceCollection
        {
            get;
            set;
        }

            = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the g text.
        /// </summary>
        /// <value>
        /// The g text.
        /// </value>
        [DataMember]
        public string GType
        {
            get;
            set;
        }

            = null;

        /// <summary>
        /// Gets or sets the g value.
        /// </summary>
        /// <value>
        /// The g value.
        /// </value>
        [DataMember]
        public string GValue
        {
            get;
            set;
        }

            = null;

        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// </returns>
        public int Compare(AttributeModel x, AttributeModel y)
        {
            Contract.Requires(x != null);

            return Compare(x.GType, y.GType);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            AttributeModel secondSource = (AttributeModel)obj;

            // compare on Page first TODO compare on Page?
            return string.Compare(GType, secondSource.GType, true, System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}