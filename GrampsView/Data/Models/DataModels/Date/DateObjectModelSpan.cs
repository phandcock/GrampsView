namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;
    using SharedSharp.Model;

    using System;
    using System.Diagnostics.Contracts;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Create Span version of DateObjectModel.
    /// </summary>
    /// TODO Update fields as per Schema
    public partial class DateObjectModelSpan : DateObjectModel, IDateObjectModelSpan
    {
        /// <summary>
        /// $$(cformat)$$ field.
        /// </summary>
        private string _GCformat;

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

        public DateObjectModelSpan(string aStart, string aStop, string aCFormat = null, bool aDualDated = false, string aNewYear = null, DateQuality aQuality = DateQuality.unknown)
        {
            Contract.Requires(!string.IsNullOrEmpty(aStart));
            Contract.Requires(!string.IsNullOrEmpty(aStop));

            // Setup basics
            ModelItemGlyph.Symbol = Constants.IconDate;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
            //DerivedType = DateObjectModelDerivedTypeEnum.DateObjectModelSpan;

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();

            // check for date range
            try
            {
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
                GStop = new DateObjectModelVal(aStop); ;

                // Set NotionalDate
                NotionalDate = NotionalDate = ConvertRFC1123StringToDateTime(aStart);
            }
            catch (Exception e)
            {
                // TODO
                App.Current.Services.GetService<IErrorNotifications>().NotifyException("Error in SetDate", e);
                throw;
            }
        }

        public DateObjectModelSpan()
        {
        }

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

        public bool GDualdated
        {
            get;

            internal set;
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
                    // Calculate the age
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
                    return GStart + " to " + GStop;
                }
                else
                {
                    return "Unknown";
                }
            }
        }

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

        public DateQuality GQuality
        {
            get => _GQuality;

            internal set => SetProperty(ref _GQuality, value);
        }

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
        public HLinkDateModelSpan HLink
        {
            get
            {
                HLinkDateModelSpan t = new HLinkDateModelSpan
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
                string dateString = $"From {GStart.LongDate} to {GStop.LongDate}";

                // Do not display a messgae if thw quality is unknown
                if (GQuality != DateQuality.unknown)
                {
                    dateString += " " + GQuality.ToString();
                }

                if (!string.IsNullOrEmpty(GCformat))
                {
                    dateString = $"{dateString} Format: {GCformat}";
                }

                if (GDualdated)
                {
                    dateString += " Dual dated";
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

                string dateString = $"{GStart.ShortDate}-{GStop.ShortDate}";
                return dateString.Trim();
            }
        }

        /// <summary>
        /// Gets returns a single dateversion of the date field Because the field can have one or
        /// two dates etc this is trickier than it sounds. We use the start date as a default.
        /// </summary>
        /// <value>
        /// The single date.
        /// </value>
        public override DateTime SingleDate
        {
            get
            {
                return NotionalDate;
            }
        }

        /// <summary>
        /// Gets returns a sortable version of the date field Because the field can have one or two
        /// dates etc this is trickier than it sounds. We use the start date.
        /// </summary>
        public override DateTime SortDate
        {
            get
            {
                return NotionalDate;
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

                if (!(string.IsNullOrEmpty(argTitle)))
                {
                    DateModelCard.Title = argTitle;
                }
            }

            return DateModelCard;
        }

        public override HLinkBase AsHLink(string argTitle)
        {
            HLinkDateModelSpan t = this.HLink;
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