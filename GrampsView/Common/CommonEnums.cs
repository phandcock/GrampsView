namespace GrampsView.Common
{
    public static class CommonEnums
    {
        public enum DateValType
        {
            about,
            after,
            before,
            unknown
        }

        public enum DisplayFormat
        {
            Default,
            HeaderCardLarge,
            InstructionCardLarge,
            MediaCardLarge,
            MediaImageFullCard,
            NoteCardFull,
            PersonNameCardSingle,
            PersonNameCardSmall,
            SourceCardSmall,
        }

        public enum HomeImageType
        {
            Symbol,
            ThumbNail,
            Unknown,
        }

        public enum PlaceLocation
        {
            city,
            country,
            county,
            locality,
            parish,
            phone,
            postal,
            state,
            street,
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