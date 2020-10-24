namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using System;
    using System.Runtime.Serialization;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Create Val version of DateObjectModel.
    /// </summary>
    /// TODO Update fields as per Schema
    [DataContract]
    public class DateObjectModelVal : DateObjectModel, IDateObjectModelVal
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
        private CommonEnums.DateQuality _GQuality = DateQuality.unknown;

        /// <summary>
        /// $$(val)$$ field.
        /// </summary>
        private string _GVal = string.Empty;

        private DateValType _GValType = DateValType.unknown;

        public DateObjectModelVal(string aVal, string aCFormat, bool aDualDated, string aNewYear, CommonEnums.DateQuality aQuality, CommonEnums.DateValType aValType)
        {
            {
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
                    // TODO
                    DataStore.CN.NotifyException("Error in SetDate", e);
                    throw;
                }
            }
        }

        public DateObjectModelVal()
        {
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

        public override int GetAge
        {
            get
            {
                int outputAge;

                // Calculate the age - ROUGHLY
                DateTime today = DateTime.Today;
                outputAge = ((today - NotionalDate).Days) / 365;

                return outputAge;
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
                if (GQuality == DateQuality.unknown)
                {
                    return string.Empty;
                }

                return " ( " + nameof(GQuality) + ")";
            }
        }

        /// <summary>
        /// Gets the $$(val)$$ field.
        /// </summary>
        [DataMember]
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
        [DataMember]
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

        public string GValTypeDecoded
        {
            get
            {
                if (GValType == DateValType.unknown)
                {
                    return string.Empty;
                }

                return nameof(GValType) + " ";
            }
        }

        public override string LongDate
        {
            get
            {
                string dateString;

                dateString = GVal;

                if (!string.IsNullOrEmpty(GCformat))
                {
                    dateString += " Format: " + GCformat;
                }

                dateString = GValTypeDecoded + dateString;

                dateString += GQualityDecoded;

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
                string dateString;

                dateString = GVal;

                dateString = GValTypeDecoded + dateString;

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

        public override CardListLineCollection AsCardListLine(string argTitle = null)
        {
            CardListLineCollection DateModelCard = new CardListLineCollection();

            if (this.Valid)
            {
                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date Type:", "Val"),
                                new CardListLine("Date:", this.LongDate),
                                new CardListLine("Val:", this.GVal),
                                new CardListLine("C Format:", this.GCformat),
                                new CardListLine("Type:", this.GValTypeDecoded),
                                new CardListLine("Quality:", this.GQualityDecoded),
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