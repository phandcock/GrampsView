﻿namespace GrampsView.Data.Model
{
    using System;
    using System.Runtime.Serialization;

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

        public DateObjectModelRange(string aCFormat, bool aDualDated, string aNewYear, DateQuality aQuality, string aStart, string aStop)
        {
            GCformat = aCFormat;

            // dualdated value #REQUIRED
            GDualdated = aDualDated;

            // newyear CDATA #IMPLIED
            GNewYear = aNewYear;

            // type CDATA #REQUIRED
            GQuality = aQuality;

            // start CDATA #REQUIRED
            GStart = new DateObjectModelVal(aStart, aCFormat, aDualDated, aNewYear, aQuality, DateValType.unknown);

            // stop CDATA #REQUIRED
            GStop = new DateObjectModelVal(aStop, aCFormat, aDualDated, aNewYear, aQuality, DateValType.unknown); ;

            // Set NotionalDate
            NotionalDate = ConvertRFC1123StringToDateTime(aStart);

            HLinkKey.Value = Guid.NewGuid().ToString();
        }

        public DateObjectModelRange()
        {
        }

        /// <summary>
        /// Gets the $$(cformat)$$ field.
        /// </summary>
        [DataMember]
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
        [DataMember]
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
                    return GStart + " to " + GStop;
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
        [DataMember]
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
        [DataMember]
        public DateQuality GQuality
        {
            get => _GQuality;

            internal set => SetProperty(ref _GQuality, value);
        }

        /// <summary>
        /// Gets the Date Start.
        /// </summary>
        [DataMember]
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
        [DataMember]
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
                string dateString = $"From {GStart.ShortDate} to {GStop.ShortDate}";

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
                string dateString = $"{GStart.ShortDate} -{GStop.ShortDate} Range";
                return dateString.Trim();
            }
        }

        public override DateTime SingleDate
        {
            get
            {
                // TODO Is this right?
                return NotionalDate;
            }
        }

        public override DateTime SortDate
        {
            get
            {
                // TODO Is this right?
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
            }

            if (!string.IsNullOrEmpty(argTitle))
            {
                DateModelCard.Title = argTitle;
            }

            return DateModelCard;
        }

        public override HLinkBase AsHLink(string argTitle)
        {
            return new HLinkDateModelRange
            {
                DeRef = this,
                HLinkGlyphItem = ModelItemGlyph,
                HLinkKey = HLinkKey,
                Title = argTitle,
            };
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