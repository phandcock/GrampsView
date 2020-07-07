//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="INoteModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using GrampsView.Data.Collections;

using Xamarin.Forms;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Note elements.
    /// </summary>
    public interface INoteModel : IModelBase
    {
        new string GetDefaultText
        {
            get;
        }

        FormattedString GFormattedTextMedium
        {
            get;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        FormattedString GFormattedTextSmall
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NoteModel"/> is format (0|1) #IMPLIED.
        /// </summary>
        /// <value>
        /// <c>true</c> if format; otherwise, <c>false</c>.
        /// </value>
        bool GIsFormated
        {
            get;

            set;
        }

        StyledTextModelCollection GStyledTextCollection { get; set; }

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        HLinkTagModelCollection GTagRefCollection
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        string GText
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the type CDATA #REQUIRED.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        string GType
        {
            get;

            set;
        }

        /// <summary>
        /// Gets the get h link Note Model that points to this ViewModel.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        HLinkNoteModel HLink { get; }

        /// <summary>
        /// Gets the shortened form of the text. Maximum length is 100.
        /// </summary>
        /// <value>
        /// The text short.
        /// </value>
        string TextShort
        {
            get;
        }
    }
}