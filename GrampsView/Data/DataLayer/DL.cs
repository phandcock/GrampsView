// Copyright (c) phandcock. All rights reserved.

using GrampsView.Data.DataLayer;
using GrampsView.Data.DataLayer.Interfaces;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// </summary>
    public static class DL
    {
        public static ICitationDataLayer CitationDL { get; } = new CitationDataLayer();

        public static IEventDataLayer EventDL { get; } = new EventDataLayer();

        public static IFamilyDataLayer FamilyDL { get; } = new FamilyDataLayer();

        /// <summary>
        /// Gets the note dv.
        /// </summary>
        /// <value>
        /// The note dv.
        /// </value>
        public static INoteDataLayer NoteDL { get; } = new NoteDataLayer();
    }
}