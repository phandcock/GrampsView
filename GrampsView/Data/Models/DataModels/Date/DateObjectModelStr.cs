// <copyright file="DateObjectModelStr.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using GrampsView.Data.Repository;

    using System;

    /// <summary>
    /// Create Str version of DateObjectModel.
    /// </summary>

    public  class DateObjectModelStr : DateObjectModel, IDateObjectModel
    {
        /// <summary>
        /// Not a properly formatted date so return 0;
        /// </summary>
        public override int GetAge
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Not a DateTime so return the String GVal.
        /// </summary>
        /// <value>
        /// The get long date as string.
        /// </value>
        public override string LongDate
        {
            get
            {
                return GVal;
            }
        }

        public override CardListLineCollection AsCardListLine(string argTitle = null)
        {

            CardListLineCollection DateModelCard = new CardListLineCollection();

            if (this.Valid)
            {
              
                
                            DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date Type:", "String"),
                                new CardListLine("Notional Date:", this.LongDate),
                                new CardListLine("Val:", this.GVal),
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
        public override  string ShortDate
        {
            get
            {
                return GVal;
            }
        }

        public override  string GetYear
        {
            get
            {
                if (Valid)
                {
                    return GVal;
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateObjectModelStr"/> class. Date but
        /// stored as a string so can not be converted to a DateTime.
        /// </summary>
        /// <param name="aCFormat">
        /// a c format.
        /// </param>
        /// <param name="aDualDated">
        /// if set to <c>true</c> [a dual dated].
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
        public  DateObjectModelStr(string aVal)
        {
            try
            {
                GVal = aVal;

                // Set NotionalDate
                NotionalDate = DateTime.MinValue;

                Valid = true;
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