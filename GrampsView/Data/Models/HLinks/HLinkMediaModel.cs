﻿//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkMediaModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

//// gramps XML 1.71 - Done
////
//// HLink
//// Priv
//// region
//// attribute
//// citationref
//// noteref

/// TODO Update fields as per Schema

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkMediaModel : HLinkBase, IHLinkMediaModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HLinkMediaModel"/> class.
        /// </summary>
        public HLinkMediaModel()
        {
        }

        /// <summary>
        /// Gets the associated media model
        /// </summary>
        /// <value>
        /// The media model. <note type="caution">This can not hold a local copy of the media model
        /// as the Model Base has a hlinkmediamodel in it and this will cause a referene loop</note>
        /// </value>
        public IMediaModel DeRef
        {
            get
            {
                if (Valid)
                {
                    return DV.MediaDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new MediaModel();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Attribute.
        /// </summary>
        /// <value>
        /// The Attribute.
        /// </value>
        [DataMember]
        public OCAttributeModelCollection GAttributeRefCollection { get; set; } = new OCAttributeModelCollection();

        /// <summary>
        /// Gets or sets the g citation model collection.
        /// </summary>
        /// <value>
        /// The g citation model collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection { get; set; } = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the g note model collection.
        /// </summary>
        /// <value>
        /// The g note model collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection { get; set; } = new HLinkNoteModelCollection();

     

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid. <note
        /// type="note">Can have a HLink or be a pointer to an image. <br/><br/> So, MUST be valid
        /// for both types and MUST be invalid for a default new instance. <br/></note>
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        public override bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(HLinkKey);
            }
        }
    }
}