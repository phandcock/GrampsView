// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.DBModels;
using GrampsView.Models.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

namespace GrampsView.Data.DataLayer.Interfaces
{
    /// <summary>
    /// Interface for the Note Repository.
    /// </summary>
    public interface INoteDataLayer : IDataLayerBase<NoteDBModel, HLinkNoteDBModel, HLinkNoteDBModelCollection>
    {
        /// <summary>
        /// Gets all as HLink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkNoteDBModelCollection GetAllAsHLink();

        /// <summary>
        /// Gets all notes of type.
        /// </summary>
        /// <param name="argType">
        /// Note type of the argument.
        /// </param>
        /// <returns>
        /// HLinkNoteModel
        /// </returns>
        DBCardGroupModel<NoteDBModel> GetAllOfType(string argType);

        List<SearcHandlerItem> SearchShell(string argQuery);

        DBCardGroupHLink<HLinkNoteDBModel> SearchTag(string queryString);
    }
}