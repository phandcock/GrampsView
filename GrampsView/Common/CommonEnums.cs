namespace GrampsView.Common
{
    public static class CommonEnums
    {
        /// <summary>
        /// Enum for the types of dates.
        /// </summary>
        public enum DateType
        {
            Range,
            Span,
            Str,
            Unknown,
            Val,
        }

        public enum DisplayFormat
        {
            Default,
            HeaderCardLarge,
            InstructionCardLarge,
            MediaCardLarge,
            MediaImageFullCard,
            NoteCardFull,
            SourceCardSmall,
        }

        public enum HomeImageType
        {
            Symbol,
            ThumbNail,
            Unknown,
        }

        public enum TextStyle
        {
            bold,
            fontcolor,
            fontface,
            fontsize,
            highlight,
            italic,
            link,
            superscript,
            underline,
            unknown,
        }

        public enum URIType
        {
            Map,
            URL
        }
    }
}