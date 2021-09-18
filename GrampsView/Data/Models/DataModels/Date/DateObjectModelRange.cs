namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System;
    using System.Diagnostics.Contracts;

    using static GrampsView.Common.CommonEnums;

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

        private DateObjectModelVal _GStart = new DateObjectModelVal();

        private DateObjectModelVal _GStop = new DateObjectModelVal();

        public DateObjectModelRange(string aStart, string aStop, string aCFormat = null, bool aDualDated = false, string aNewYear = null, DateQuality aQuality = DateQuality.unknown)
        {
            Contract.Requires(!String.IsNullOrEmpty(aStart));
            Contract.Requires(!String.IsNullOrEmpty(aStop));

            // Setup basics
            ModelItemGlyph.Symbol = CommonConstants.IconDate;
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

        /// <summary>
        /// Gets the $$(cformat)$$ field.
        /// </summary>

        public string GCformat
        {
            get => _GCformat;

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _GCformat, value);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether gets or sets the $$(dualdated)$$ field.
        /// </summary>

        public bool GDualdated
        {
            get => _GDualdated;

            internal set => SetProperty(ref _GDualdated, value);
        }

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

        public override string GetYear
        {
            get
            {
                if (Valid)
                {
                    return $"Between {GStart.GetYear} and {GStop.GetYear}";
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        /// <summary>
        /// Gets the New Year field.
        /// </summary>

        public string GNewYear
        {
            get => _GNewYear;

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _GNewYear, value);
                }
            }
        }

        /// <summary>
        /// Get the Date Quality.
        /// </summary>

        public DateQuality GQuality
        {
            get => _GQuality;

            internal set => SetProperty(ref _GQuality, value);
        }

        /// <summary>
        /// Gets the Date Start.
        /// </summary>

        public DateObjectModelVal GStart
        {
            get
            {
                return _GStart;
            }

            internal set
            {
                if (value.Valid)
                {
                    SetProperty(ref _GStart, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Stop field.
        /// </summary>

        public DateObjectModelVal GStop
        {
            get
            {
                return _GStop;
            }

            internal set
            {
                if (value.Valid)
                {
                    SetProperty(ref _GStop, value);
                }
            }
        }

        public HLinkDateModelRange HLink
        {
            get
            {
                HLinkDateModelRange t = new HLinkDateModelRange
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

                string dateString = $"Range {GStart.ShortDate} -{GStop.ShortDate}";
                return dateString.Trim();
            }
        }

        public override CardListLineCollection AsCardListLine(string argTitle = "Date Detail")
        {
            CardListLineCollection DateModelCard = new CardListLineCollection();

            if (this.Valid)
            {
                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date:", this.LongDate),
                                new CardListLine("Start:", this.GStart.ShortDate),
                                new CardListLine("Stop:", this.GStop.ShortDate),
                                new CardListLine("Quality:", this.GQuality.ToString(),this.GQuality != DateQuality.unknown),
                                new CardListLine("C Format:", this.GCformat),
                                new CardListLine("Dual Dated:", this.GDualdated,true),
                                new CardListLine("New Year:", this.GNewYear),
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
            HLinkDateModelRange t = this.HLink;
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

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            DateObjectModel tempObj = obj as DateObjectModel;

            return this.NotionalDate == tempObj.NotionalDate;
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}