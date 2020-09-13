namespace GrampsView.Data.Model
{
    using System;

    /// <summary>
    /// Create Val version of DateObjectModel.
    /// TODO Update fields as per Schema
    /// </summary>

    public partial class DateObjectModelRange : DateObjectModel, IDateObjectModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateObjectModelRange" /> class.
        /// </summary>
        /// <param name="aType">
        /// a type.
        /// </param>
        /// <param name="aCFormat">
        /// a c format.
        /// </param>
        /// <param name="aDualDated">
        /// if set to <c> true </c> [a dual dated].
        /// </param>
        /// <param name="aNewYear">
        /// a new year.
        /// </param>
        /// <param name="aQuality">
        /// a quality.
        /// </param>
        /// <param name="aStart">
        /// a start.
        /// </param>
        /// <param name="aStop">
        /// a stop.
        /// </param>
        /// <param name="aVal">
        /// a value.
        /// </param>
        /// <param name="aValType">
        /// Type of a value.
        /// </param>

        //private string localGValType;

        public override int GetAge
        {
            get
            {
                int outputAge;

                // calculate the age
                DateTime today = DateTime.Today;
                outputAge = today.Year - NotionalDate.Year;

                return outputAge;
            }
        }

        public override string LongDate
        {
            get
            {
                string dateString = "Range from " + GStart + " to " + GStop;

                if (!string.IsNullOrEmpty(GQuality))
                {
                    dateString += GQuality;
                }

                if (!string.IsNullOrEmpty(GCformat))
                {
                    dateString += " Format: " + GCformat;
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

        public override CardListLineCollection AsCardListLine(string argTitle = null)
        {

            CardListLineCollection DateModelCard = new CardListLineCollection();

            if (this.Valid)
            {
            
                            DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date Type:", "Range"),
                                new CardListLine("Notional Date:", this.LongDate),
                                new CardListLine("Start:", this.GStart),
                                new CardListLine("Stop:", this.GStop),
                                new CardListLine("Quality:", this.GQuality),
                                new CardListLine("C Format:", this.GCformat),
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

        public  DateObjectModelRange(string aCFormat, bool aDualDated, string aNewYear, string aQuality, string aStart, string aStop)
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
    }
}