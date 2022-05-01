namespace GrampsView.Data.Model
{
    public class SearcHandlerItem
    {
        private HLinkBackLink baseBackLink = new HLinkBackLink();

        public SearcHandlerItem(HLinkNoteModel argHLinkNoteModel)
        {
            baseBackLink = new HLinkBackLink(argHLinkNoteModel);
        }

        public SearcHandlerItem(HLinkPersonModel argHLinkPersonModel)
        {
            baseBackLink = new HLinkBackLink(argHLinkPersonModel);
        }

        public string DefaultShortText
        {
            get
            {
                if (baseBackLink.HLink.GetType() == typeof(HLinkNoteModel))
                {
                    return (baseBackLink.HLink as HLinkNoteModel).DeRef.DefaultTextShort;
                }

                if (baseBackLink.HLink.GetType() == typeof(HLinkPersonModel))
                {
                    return (baseBackLink.HLink as HLinkPersonModel).DeRef.DefaultTextShort;
                }

                return "??? Unknown Type???";
            }
        }

        public HLinkBase HLink
        {
            get
            {
                return baseBackLink.HLink;
            }
        }

        public override string ToString()
        {
            return DefaultShortText;
        }
    }
}