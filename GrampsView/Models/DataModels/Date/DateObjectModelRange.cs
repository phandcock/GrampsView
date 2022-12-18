using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.HLinks;


using SharedSharp.Models;

using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.Models.DataModels.Date
{
    /// <summary>
    /// Create Range version of DateObjectModel. TODO Update fields as per Schema
    /// </summary>

    public class DateObjectModelRange : DateObjectModel, IDateObjectModelRange
    {
        /// <summary>
        /// $$(cformat)$$ field.
        /// </summary>
        private string _GCformat = string.Empty;

        /// <summary>
        /// Dual dated field.
        /// </summary>
        private bool _GDualdated;

        /// <summary>
        /// New year field.
        /// </summary>
        private string _GNewYear = string.Empty;

        /// <summary>
        /// Quality field.
        /// </summary>
        private DateQuality _GQuality = DateQuality.unknown;

        private DateObjectModelVal _GStart = new();

        private DateObjectModelVal _GStop = new();

        public DateObjectModelRange(string aStart, string aStop, string? aCFormat = null, bool aDualDated = false, string? aNewYear = null, DateQuality aQuality = DateQuality.unknown)
        {
            Contract.Requires(!string.IsNullOrEmpty(aStart));
            Contract.Requires(!string.IsNullOrEmpty(aStop));

            // Setup basics
            ModelItemGlyph.Symbol = Constants.IconDate;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
            //DerivedType = DateObjectModelDerivedTypeEnum.DateObjectModelRange;

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();

            GCformat = aCFormat;

            // dualdated value #REQUIRED
            GDualdated = aDualDated;

            // newyear CDATA #IMPLIED
            GNewYear = aNewYear;

            // type CDATA #REQUIRED
            GQuality = aQuality;

            // start CDATA #REQUIRED
            GStart = new DateObjectModelVal(aStart);

            // stop CDATA #REQUIRED
            GStop = new DateObjectModelVal(aStop);

            // Set NotionalDate
            NotionalDate = ConvertRFC1123StringToDateTime(aStart);
        }

        public DateObjectModelRange()
        {
        }

        public string GCformat
        {
            get => _GCformat;

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _ = SetProperty(ref _GCformat, value);
                }
            }
        }

        public bool GDualdated
        {
            get => _GDualdated;

            internal set => SetProperty(ref _GDualdated, value);
        }

        /// <summary>
        /// Gets the $$(cformat)$$ field.
        /// </summary>
        /// <summary>
        /// Gets a value indicating whether gets or sets the $$(dualdated)$$ field.
        /// </summary>
        public override int? GetAge
        {
            get
            {
                if (Valid)
                {
                    // calculate the age
                    DateTime today = DateTime.Today;
                    return today.Year - NotionalDate.Year;
                }

                return null;
            }
        }

        public override string GetYear => Valid ? $"Between {GStart.GetYear} and {GStop.GetYear}" : "Unknown";

        public string GNewYear
        {
            get => _GNewYear;

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _ = SetProperty(ref _GNewYear, value);
                }
            }
        }

        public DateQuality GQuality
        {
            get => _GQuality;

            internal set => SetProperty(ref _GQuality, value);
        }

        public DateObjectModelVal GStart
        {
            get => _GStart;

            internal set
            {
                if (value.Valid)
                {
                    _ = SetProperty(ref _GStart, value);
                }
            }
        }

        public DateObjectModelVal GStop
        {
            get => _GStop;

            internal set
            {
                if (value.Valid)
                {
                    _ = SetProperty(ref _GStop, value);
                }
            }
        }

        /// <summary>
        /// Gets the New Year field.
        /// </summary>
        /// <summary>
        /// Get the Date Quality.
        /// </summary>
        /// <summary>
        /// Gets the Date Start.
        /// </summary>
        /// <summary>
        /// Gets or sets the Stop field.
        /// </summary>

        [JsonIgnore]
        public HLinkDateModelRange HLink
        {
            get
            {
                HLinkDateModelRange t = new()
                {
                    DeRef = this,
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        public override string LongDate
        {
            get
            {
                if (!Valid)
                {
                    return string.Empty;
                }

                string dateString = $"Between {GStart.LongDate} and {GStop.LongDate}";

                if (GQuality != DateQuality.unknown)
                {
                    dateString += " ( " + GQuality + " )";
                }

                if (!string.IsNullOrEmpty(GCformat))
                {
                    dateString += " Format: " + GCformat;
                }

                if (GDualdated)
                {
                    dateString += " (Dual dated)";
                }

                if (!string.IsNullOrEmpty(GNewYear))
                {
                    dateString += " New Year: " + GNewYear;
                }

                return dateString.Trim();
            }
        }

        /// <summary>
        /// Gets the string version of the date field.
        /// </summary>
        /// <returns>
        /// a string version of the date.
        /// </returns>
        public override string ShortDate
        {
            get
            {
                if (!Valid)
                {
                    return string.Empty;
                }

                string dateString = $"Range {GStart.ShortDate} - {GStop.ShortDate}";
                return dateString.Trim();
            }
        }

        public override CardListLineCollection AsCardListLine(string argTitle = "Date Detail")
        {
            CardListLineCollection DateModelCard = new();

            if (Valid)
            {
                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date:", LongDate),
                                new CardListLine("Start:", GStart.ShortDate),
                                new CardListLine("Stop:", GStop.ShortDate),
                                new CardListLine("Quality:", GQuality.ToString(),GQuality != DateQuality.unknown),
                                new CardListLine("C Format:", GCformat),
                                new CardListLine("Dual Dated:", GDualdated,true),
                                new CardListLine("New Year:", GNewYear),
                            };
            }

            if (!string.IsNullOrEmpty(argTitle))
            {
                DateModelCard.Title = argTitle;
            }

            return DateModelCard;
        }

        public override HLinkBase AsHLink(string argTitle)
        {
            HLinkDateModelRange t = HLink;
            t.Title = argTitle;

            return t;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            DateObjectModel? tempObj = obj as DateObjectModel;

            return NotionalDate == tempObj.NotionalDate;
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}