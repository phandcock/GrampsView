// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.DataLayer;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// </summary>
    public static class DL
    {
        /// <summary>
        /// Gets the note dv.
        /// </summary>
        /// <value>
        /// The note dv.
        /// </value>
        public static INoteDataLayer NoteDL { get; } = new NoteDataLayer();
    }
}