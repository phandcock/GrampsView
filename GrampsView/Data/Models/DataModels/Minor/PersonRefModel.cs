//-----------------------------------------------------------------------
//
// Handles GRAMPS Alt fields
//
// <copyright file="SrcAttributeModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using GrampsView.Data.Collections;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS Alt element class.
    /// </summary>
    public class PersonRefModel : ModelBase, IPersonRefModel
    {
        public HLinkCitationModelCollection GCitationCollection
        {
            get;
        }

            = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the g text.
        /// </summary>
        /// <value>
        /// The g text.
        /// </value>
        public HLinkNoteModelCollection GNoteCollection
        {
            get;
        }

            = new HLinkNoteModelCollection();
    }
}