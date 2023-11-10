// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.ModelsDB.HLinks.Models;

namespace GrampsView.Models.DBModels.Interfaces
{
    public interface INoteDBModel : IDBModelBase
    {
        public string GType { get; set; }

        public bool GIsFormated { get; set; }

        StyledTextModel GStyledText { get; set; }

        HLinkTagModelCollection GTagRefCollection { get; set; }

        HLinkNoteDBModel HLink
        {
            get;
        }
    }
}