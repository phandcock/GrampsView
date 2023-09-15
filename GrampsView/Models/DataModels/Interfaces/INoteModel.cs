// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.Models.DataModels;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Note elements.
    /// </summary>
    public interface INoteModel : IModelBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NoteModel"/> is format (0|1) #IMPLIED.
        /// </summary>
        /// <value>
        /// <c> true </c> if format; otherwise, <c> false </c>.
        /// </value>
        bool GIsFormated
        {
            get;

            set;
        }

        StyledTextModel GStyledText
        {
            get;
        }

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        HLinkTagModelCollection GTagRefCollection
        {
            get;
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

        HLinkNoteModel HLink
        {
            get;
        }
    }
}