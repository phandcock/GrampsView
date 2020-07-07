// <copyright file="DateObjectModelVal.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using System;
    using System.Runtime.Serialization;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    /// <summary>
    /// Create Val version of DateObjectModel.
    /// </summary>

    public partial class DateObjectModel
    {
        /// <summary>
        /// The value g value type
        /// </summary>
        private string _ValGValType;

        public int ValGetAge
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

        public string ValGetLongDateAsString
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
                dateString = GValType + " " + dateString;

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
        public string ValGetShortDateAsString
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

        public string ValGetYear
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
        /// Gets the type of the g value.
        /// </summary>
        /// <value>
        /// The type of the g value.
        /// </value>
        [DataMember]
        public string ValGValType
        {
            get
            {
                return _ValGValType;
            }

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _ValGValType, value);
                }
            }
        }

        public void DateObjectModelVal(string aValType)

        {
            try
            {
                // type
                GValType = aValType;

                // Set NotionalDate
                NotionalDate = ConvertRFC1123StringToDateTime(GVal);
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