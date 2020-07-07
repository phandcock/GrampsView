// <copyright file="DateObjectModelSpan.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using GrampsView.Data.Repository;

    using System;

    /// <summary>
    /// Create Val version of DateObjectModel.
    /// </summary>

    public partial class DateObjectModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateObjectModelSpan" /> class.
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

        public int SpanGetAge
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

        public string SpanGetLongDateAsString
        {
            get
            {
                string dateString = "Between " + GStart + "-" + GStop;

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

        /// <summary>
        /// Gets the string version of the date field.
        /// </summary>
        /// <returns>
        /// a string version of the date.
        /// </returns>
        public string SpanGetShortDateAsString
        {
            get
            {
                string dateString = "Between " + GStart + "-" + GStop;
                return dateString.Trim();
            }
        }

        public string SpanGetYear
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

        public void DateObjectModelSpan(string aCFormat, bool aDualDated, string aNewYear, string aQuality, string aStart, string aStop, string aVal)
        {
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
                GStart = aStart;

                // stop CDATA #REQUIRED
                GStop = aStop;

                // Set NotionalDate
                NotionalDate = ConvertRFC1123StringToDateTime(GStart);
            }
            catch (Exception e)
            {
                // TODO
                DataStore.CN.NotifyException("Error in SetDate", e);
                throw;
            }
        }
    }
}