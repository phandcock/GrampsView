﻿// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks;
using GrampsView.Views;

namespace GrampsView.Data.Model
{
    public class HLinkNoteModel : HLinkBase, IHLinkNoteModel
    {
        private NoteModel _Deref = new NoteModel();

        private bool DeRefCached = false;

        public HLinkNoteModel()
        {
            HLinkGlyphItem.Symbol = Constants.IconNotes;
            HLinkGlyphItem.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNote");
        }

        public NoteModel DeRef
        {
            get
            {
                if (Valid && (!DeRefCached))
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