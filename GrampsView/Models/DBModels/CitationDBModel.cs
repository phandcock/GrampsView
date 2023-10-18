// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.DataModels;

namespace GrampsView.Models.DBModels
{
    public class CitationDBModel : DBModel<CitationModel, HLinkCitationModel>, IDBModel<CitationModel, HLinkCitationModel>
    {
        public CitationDBModel()
        {
        }

        public CitationDBModel(CitationModel argNoteModel)
        {
            Serialise(argNoteModel);
        }
    }
}