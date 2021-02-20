using System.ComponentModel;

namespace GrampsView.Common
{
    public static class CommonEnums
    {
        public enum DateQuality
        {
            calculated,
            estimated,
            unknown
        }

        public enum DateValType
        {
            [Description("About")]
            about,

            [Description("After")]
            after,

            [Description("Before")]
            before,

            [Description("Unknown")]
            unknown
        }

        public enum DisplayFormat
        {
            Default,
            HeaderCardLarge,
            NoteCardFull,
            NoteCardSmall,
            SourceCardSmall,
        }

        public enum Gender
        {
            Female,
            Male,
            Unknown
        }

        public enum HLinkGlyphType
        {
            Image,
            Media,
            Symbol,
            Unknown,
        }

        //public enum ModelDisplayType
        //{
        //    Image,
        //    Media,
        //    Symbol,
        //    Unknown,
        //}

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