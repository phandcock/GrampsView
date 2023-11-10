// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

namespace GrampsView.Common.CustomClasses
{
    public class SearcHandlerItem : IComparable
    {
        private HLinkDBBackLink baseBackLink = new HLinkDBBackLink();

        public SearcHandlerItem(HLinkNoteDBModel argHLinkNoteModel)
        {
            baseBackLink = new HLinkDBBackLink(argHLinkNoteModel);
        }



        public SearcHandlerItem(HLinkPersonModel argHLinkPersonModel)
        {
            baseBackLink = new HLinkDBBackLink(argHLinkPersonModel);
        }

        public string DefaultShortText
        {
            get
            {
                if (baseBackLink.HLink.GetType() == typeof(HLinkNoteDBModel))
                {
                    return (baseBackLink.HLink as HLinkNoteDBModel).DeRef.DefaultTextShort;
                }

                // Fix when person changed to DBModel type
                //if (baseBackLink.HLink.GetType() == typeof(HLinkPersonModel))
                //{
                //    return (baseBackLink.HLink as HLinkPersonModel).DeRef.DefaultTextShort;
                //}

                return "??? Unknown Type???";
            }
        }

        public HLinkDBBase HLink
        {
            get
            {
                return baseBackLink.HLink;
            }
        }

        public int CompareTo(object obj)
        {
            return string.Compare(this.DefaultShortText, (obj as SearcHandlerItem).DefaultShortText);
        }

        public override string ToString()
        {
            return DefaultShortText;
        }
    }
}