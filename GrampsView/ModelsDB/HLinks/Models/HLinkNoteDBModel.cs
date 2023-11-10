// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.DBModels;
using GrampsView.Models.HLinks;
using GrampsView.ModelsDB.HLinks.Interfaces;
using GrampsView.Views;

namespace GrampsView.ModelsDB.HLinks.Models
{
    public class HLinkNoteDBModel : HLinkDBBase, IHLinkNoteDBModel
    {
        private NoteDBModel _Deref = new NoteDBModel();

        private bool DeRefCached = false;

        public HLinkNoteDBModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconNotes;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNote");
        }

        public NoteDBModel DeRef
        {
            get
            {
                if (Valid && !DeRefCached)
                {
                    _Deref = DL.NoteDL.GetModelFromHLinkKey(HLinkKey);

                    if (_Deref.Valid)
                    {
                        DeRefCached = true;
                    }
                }

                return _Deref;
            }
        }

        public override Page NavigationPage()
        {
            return new NoteDetailPage(this);
        }
    }
}