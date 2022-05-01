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
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    using System.Collections.Generic;

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
        /// Gets all notes of type.
        /// </summary>
        /// <param name="argType">
        /// Note type of the argument.
        /// </param>
        /// <returns>
        /// HLinkNoteModel
        /// </returns>
        CardGroupModel<NoteModel> GetAllOfType(string argType);

        List<SearcHandlerItem> SearchShell(string argQuery);

        CardGroupHLink<HLinkNoteModel> SearchTag(string queryString);
    }
}