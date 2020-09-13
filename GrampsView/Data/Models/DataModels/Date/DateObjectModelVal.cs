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
    public class DateObjectModelVal : DateObjectModel, IDateObjectModel
    {
        private DateValType _GValType = DateValType.unknown;

        public DateObjectModelVal(string aVal)

        {
            try
            {
                GVal = aVal;

                // type TODO fix this
                GValType = Common.CommonEnums.DateValType.unknown;

                // Set NotionalDate
                NotionalDate = ConvertRFC1123StringToDateTime(aVal);
            }
            catch (Exception e)
            {
                // TODO
                DataStore.CN.NotifyException("Error in SetDate", e);
                throw;
            }
        }

        public DateObjectModelVal()

        {
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

        public string GValTypeAsString
        {
            get
            {
                switch (GValType)
                {
                    case CommonEnums.DateValType.about:
                        {
                            return "About";
                        }

                    case CommonEnums.DateValType.before:
                        {
                            return "Before";
                        }

                    case CommonEnums.DateValType.after:
                        {
                            return "After";
                        }

                    default:
                        {
                            break;
                        }
                }

                return "";
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

                // Handle Type
                dateString = GValTypeAsString + " " + dateString;

                if (!string.IsNullOrEmpty(GQuality))
                {
                    dateString += GQuality;
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
                string dateString;

                dateString = GVal;

                // Handle Type
                dateString = GValType + " " + dateString;

                return dateString.Trim();
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
                                new CardListLine("Notional Date:", this.LongDate),
                                new CardListLine("Val:", this.GVal),
                                new CardListLine("C Format:", this.GCformat),
                                new CardListLine("Type:", this.GValTypeAsString),
                                new CardListLine("Quality:", this.GQuality),
                                new CardListLine("Dual Dated:", this.GDualdated),
                                new CardListLine("New Year:", this.GNewYear),
                            };
            }

            if (!(string.IsNullOrEmpty(argTitle)))
            {
                DateModelCard.Title = argTitle;
            }

            return DateModelCard;
        }
    }
}