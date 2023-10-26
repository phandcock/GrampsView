// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels.Interfaces;

namespace GrampsView.Models.DBModels
{
    public class CitationDBModel : DBModel<CitationModel, HLinkCitationModel>, ICitationDBModel<CitationModel, HLinkCitationModel>
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