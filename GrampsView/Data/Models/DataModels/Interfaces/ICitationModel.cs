//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="ICitationModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using GrampsView.Data.Collections;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Note elements.
    /// </summary>
    public interface ICitationModel : IModelBase
    {
        string GConfidence
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the content of the DateContent field.
        /// </summary>
        /// <value>
        /// The content of the g date.
        /// </value>
        DateObjectModel GDateContent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the get default text.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        new string GetDefaultText { get; }

        /// <summary>
        /// Gets or sets the media reference collection.
        /// </summary>
        /// <value>
        /// The media reference collection.
        /// </value>
        HLinkMediaModelCollection GMediaRefCollection
        {
            get;
        }

        /// <summary>
        /// Gets or sets the note reference.
        /// </summary>
        /// <value>
        /// The g note reference.
        /// </value>
        HLinkNoteModelCollection GNoteRefCollection
        {
            get;
        }

        string GPage
        {
            get;
            set;
        }

        OCSrcAttributeModelCollection GSourceAttributeCollection
        {
            get;
        }

        HLinkSourceModel GSourceRef
        {
            get;
            set;
        }

        HLinkTagModelCollection GTagRef
        {
            get;
        }

        HLinkCitationModel HLink { get; }
    }
}