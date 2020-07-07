//-----------------------------------------------------------------------
//
// Interface for the Note Repository
//
// <copyright file="INoteDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    /// <summary>
    /// Interface for the Note Repository.
    /// </summary>
    public interface INoteDataView : IDataViewBase<NoteModel, HLinkNoteModel, HLinkNoteModelCollection>
    {
        /// <summary>
        /// Gets or sets the note data.
        /// </summary>
        /// <value>
        /// The note data.
        /// </value>
        RepositoryModelDictionary<NoteModel, HLinkNoteModel> NoteData
        {
            get;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkNoteModelCollection GetAllAsHLink();

        /// <summary>
        /// Gets all t of aype.
        /// </summary>
        /// <param name="argType">
        /// Type of the argument.
        /// </param>
        /// <returns>
        /// </returns>
        ObservableCollection<NoteModel> GetAllOfType(string argType);

        List<SearchItem> SearchTag(string queryString);
    }
}