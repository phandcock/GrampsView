namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Create Range version of DateObjectModel. TODO Update fields as per Schema
    /// </summary>

    public partial class DateObjectModelRange : DateObjectModel, IDateObjectModelRange
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
        private CommonEnums.DateQuality _GQuality = CommonEnums.DateQuality.unknown;

        /// <summary>
        /// Start field.
        /// </summary>
        private string _GStart = string.Empty;

        /// <summary>
        /// Stop field.
        /// </summary>
        private string _GStop = string.Empty;

        public DateObjectModelRange(string aCFormat, bool aDualDated, string aNewYear, CommonEnums.DateQuality aQuality, string aStart, string aStop)
        {
            GCformat = aCFormat;

            // dualdated value #REQUIRED
            GDualdated = aDualDated;

            // newyear CDATA #IMPLIED
            GNewYear = aNewYear;

            // type CDATA #REQUIRED
            GQuality = aQuality;

            // start CDATA #REQUIRED
            GStart = aStart;

            // stop CDATA #REQUIRED
            GStop = aStop;

            // Set NotionalDate
            NotionalDate = ConvertRFC1123StringToDateTime(GStart);
        }

        /// <summary>
        /// Gets the $$(cformat)$$ field.
        /// </summary>
        [DataMember]
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
        [DataMember]
        public bool GDualdated
        {
            get
            {
                return _GDualdated;
            }

            internal set
            {
                SetProperty(ref _GDualdated, value);
            }
        }

        public override Nullable<int> GetAge
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
        [DataMember]
        public CommonEnums.DateQuality GQuality
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

        public string GQualityDecoded
        {
            get
            {
                if (GQuality == CommonEnums.DateQuality.unknown)
                {
                    return string.Empty;
                }

                return nameof(GQuality);
            }
        }

        /// <summary>
        /// Gets the Date Start.
        /// </summary>
        [DataMember]
        public string GStart
        {
            get
            {
                return _GStart;
            }

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _GStart, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Stop field.
        /// </summary>
        [DataMember]
        public string GStop
        {
            get
            {
                return _GStop;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _GStop, value);
                }
            }
        }

        public override string LongDate
        {
            get
            {
                string dateString = "From " + GStart + " to " + GStop;

                if (GQuality != CommonEnums.DateQuality.unknown)
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
                string dateString = "Range " + GStart + "-" + GStop;
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
                                new CardListLine("Start:", this.GStart),
                                new CardListLine("Stop:", this.GStop),
                                new CardListLine("Quality:", this.GQualityDecoded),
                                new CardListLine("C Format:", this.GCformat),
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

            return (this.NotionalDate == tempObj.NotionalDate);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}