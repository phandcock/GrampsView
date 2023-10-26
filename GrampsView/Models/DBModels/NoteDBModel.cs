// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels.Interfaces;

namespace GrampsView.Models.DBModels
{
    public class NoteDBModel : DBModel<NoteModel, HLinkNoteModel>, INoteDBModel<NoteModel, HLinkNoteModel>
    {
        public NoteDBModel()
        {
        }

        public NoteDBModel(NoteModel argNoteModel)
        {
            Serialise(argNoteModel);

            GType = argNoteModel.GType;
        }

        public string GType
        {
            get;

            set;
        } = string.Empty;
    }
}