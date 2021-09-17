namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Create Val version of DateObjectModel.
    /// </summary>
    /// TODO Update fields as per Schema

    public class DateObjectModelVal : DateObjectModel, IDateObjectModelVal
    {
        /// <summary>
        /// $$(cformat)$$ field.
        /// </summary>
        private string _GCformat = string.Empty;

        private string _GNewYear = string.Empty;

        /// <summary>
        /// Quality field.
        /// </summary>
        private DateQuality _GQuality = DateQuality.unknown;

        /// <summary>
        /// $$(val)$$ field.
        /// </summary>
        private string _GVal = string.Empty;

        private DateValType _GValType = DateValType.unknown;

        public DateObjectModelVal(string aVal, string aCFormat = null, bool aDualDated = false, string aNewYear = null, DateQuality aQuality = DateQuality.unknown, DateValType aValType = DateValType.unknown)
        {
            {
                Contract.Requires(!string.IsNullOrEmpty(aVal));

                // Setup basics
                ModelItemGlyph.Symbol = CommonConstants.IconDate;
                ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

                HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();

                try
                {
                    GCformat = aCFormat;

                    GDualdated = aDualDated;

                    GNewYear = aNewYear;

                    GQuality = aQuality;

                    GVal = aVal;

                    GValType = aValType;

                    NotionalDate = ConvertRFC1123StringToDateTime(aVal);
                }
                catch (Exception e)
                {
                    DataStore.Instance.CN.NotifyException("Error in SetDate", e);
                    throw;
                }
            }
        }

        public DateObjectModelVal()
        {
        }

        public override string DefaultTextShort
        {
            get
            {
                return ShortDate;
            }
        }

        /// <summary>
        /// Gets the $$(cformat)$$ field.
        /// </summary>

        public string GCformat
        {
            get
            {
                return _GCformat;
            }

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
            get;

            internal set;
        }

        public override int? GetAge
        {
            get
            {
                if (Valid)
                {
                    // Calculate the age - ROUGHLY
                    DateTime today = DateTime.Today;
                    return (today - NotionalDate).Days / 365;
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
                    return NotionalDate.Year.ToString(System.Globalization.CultureInfo.CurrentCulture);
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
            get
            {
                return _GNewYear;
            }

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
            get
            {
                return _GQuality;
            }

            internal set
            {
                SetProperty(ref _GQuality, value);
            }
        }

        /// <summary>
        /// Gets the $$(val)$$ field.
        /// </summary>

        public string GVal
        {
            get
            {
                return _GVal;
            }

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _GVal, value);
                }
            }
        }

        /// <summary>
        /// Gets the type of the Val Type, e.g. Before
        /// </summary>
        /// <value>
        /// The type of the g value.
        /// </value>

        public DateValType GValType
        {
            get
            {
                return _GValType;
            }

            internal set
            {
                SetProperty(ref _GValType, value);
            }
        }

        public HLinkDateModelVal HLink
        {
            get
            {
                HLinkDateModelVal t = new HLinkDateModelVal
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

                string dateString = string.Empty;

                if (NotionalDate != DateTime.MinValue)
                {
                    dateString = base.LongDate;
                }

                if (!string.IsNullOrEmpty(GCformat))
                {
                    dateString += " Format: " + GCformat;
                }

                if (GValType != DateValType.unknown)
                {
                    dateString = Enum.GetName(typeof(DateValType), GValType) + " " + dateString;
                }

                // Do not display a messgae if thw quality is unknown
                if (GQuality != DateQuality.unknown)
                {
                    dateString += " " + GQuality.ToString();
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

                string dateString = string.Empty;

                if (NotionalDate != DateTime.MinValue)
                {
                    dateString = base.ShortDate;
                }

                if (GValType != DateValType.unknown)
                {
                    dateString = Enum.GetName(typeof(DateValType), GValType) + " " + dateString;
                }

                return dateString.Trim();
            }
        }

        public override CardListLineCollection AsCardListLine(string argTitle = "Date Detail")
        {
            CardListLineCollection DateModelCard = new CardListLineCollection();

            if (this.Valid)
            {
                DateModelCard = new CardListLineCollection("Date Detail")
                            {
                                new CardListLine("Date:", this.LongDate),
                                new CardListLine("Val:", this.GVal),
                                new CardListLine("C Format:", this.GCformat),
                                new CardListLine("Type:", this.GValType.ToString(),this.GValType != DateValType.unknown),
                                new CardListLine("Quality:", this.GQuality.ToString(),this.GQuality != DateQuality.unknown),
                                new CardListLine("Dual Dated:", this.GDualdated,true),
                                new CardListLine("New Year:", this.GNewYear),
                            };
            }

            if (!(string.IsNullOrEmpty(argTitle)))
            {
                DateModelCard.Title = argTitle;
            }

            return DateModelCard;
        }

        public override HLinkBase AsHLink(string argTitle)
        {
            HLinkDateModelVal t = this.HLink;
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

            if (obj is null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            DateObjectModel tempObj = obj as DateObjectModel;

            return (this.NotionalDate == tempObj.NotionalDate);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}