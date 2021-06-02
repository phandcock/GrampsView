using System.ComponentModel;

namespace GrampsView.Common
{
    public static class CommonEnums
    {
        public enum DataConfidence
        {
            [Description("Very High")]
            VeryHigh,

            [Description("High")]
            High,

            [Description("Normal")]
            Normal,

            [Description("Low")]
            Low,

            [Description("Very Low")]
            VeryLow,

            [Description("Unknown")]
            Unknown
        }

        public enum DateQuality
        {
            [Description("Calculated")]
            calculated,

            [Description("Estimated")]
            estimated,

            [Description("Unknown")]
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
            FullCard,
            LargeCard,
            SingleCard,
            SmallCard
        }

        public enum EventModelType
        {
            [Description("Adopted")]
            ADOPT,

            [Description("Adult Christening")]
            ADULT_CHRISTEN,

            [Description("Annulment")]
            ANNULMENT,

            [Description("Baptism")]
            BAPTISM,

            [Description("Bar Mitzvah")]
            BAR_MITZVAH,

            [Description("Bas Mitzvah")]
            BAS_MITZVAH,

            [Description("Birth")]
            BIRTH,

            [Description("Blessing")]
            BLESS,

            [Description("Burial")]
            BURIAL,

            [Description("Cause Of Death")]
            CAUSE_DEATH,

            [Description("Census")]
            CENSUS,

            [Description("Christening")]
            CHRISTEN,

            [Description("Confirmation")]
            CONFIRMATION,

            [Description("Cremation")]
            CREMATION,

            [Description("Custom")]
            CUSTOM,

            [Description("Death")]
            DEATH,

            [Description("Degree")]
            DEGREE,

            [Description("Divorce Filing")]
            DIV_FILING,

            [Description("Divorce")]
            DIVORCE,

            [Description("Education")]
            EDUCATION,

            [Description("Elected")]
            ELECTED,

            [Description("Emigration")]
            EMIGRATION,

            [Description("Engagement")]
            ENGAGEMENT,

            [Description("First Communion")]
            FIRST_COMMUN,

            [Description("Graduation")]
            GRADUATION,

            [Description("Immigration")]
            IMMIGRATION,

            [Description("Alternate Marriage")]
            MARR_ALT,

            [Description("Marriage Banns")]
            MARR_BANNS,

            [Description("Marriage Contract")]
            MARR_CONTR,

            [Description("Marriage License")]
            MARR_LIC,

            [Description("Marriage Settlement")]
            MARR_SETTL,

            [Description("Marriage")]
            MARRIAGE,

            [Description("Medical Information")]
            MED_INFO,

            [Description("Military Service")]
            MILITARY_SERV,

            [Description("Naturalization")]
            NATURALIZATION,

            [Description("Nobility Title")]
            NOB_TITLE,

            [Description("Number of Marriages")]
            NUM_MARRIAGES,

            [Description("Occupation")]
            OCCUPATION,

            [Description("Ordination")]
            ORDINATION,

            [Description("Probate")]
            PROBATE,

            [Description("Property")]
            PROPERTY,

            [Description("Religion")]
            RELIGION,

            [Description("Residence")]
            RESIDENCE,

            [Description("Retirement")]
            RETIREMENT,

            [Description("Unknown")]
            UNKNOWN,

            [Description("Will")]
            WILL
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
            TempLoading,
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

        public enum RelationshipType
        {
            [Description("Civil Union")]
            civilunion,

            [Description("Custom")]
            custom,

            [Description("Married")]
            married,

            [Description("Unmarried")]
            unmarried,

            [Description("Unknown")]
            unknown
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