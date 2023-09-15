// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.DataModels;

namespace GrampsView.Models.DBModels
{
    public class NoteDBModel : DBModel<NoteModel, HLinkNoteModel>, IDBModel<NoteModel, HLinkNoteModel>
    {
        public NoteDBModel()
        {
        }

        public NoteDBModel(NoteModel argNoteModel)
        {
            Serialise(argNoteModel);
        }




    }
}