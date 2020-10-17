namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// data model for an Date object ************************************************************.
    /// <code>
    /// TODO Update fields as per Schema
    ///!ELEMENT daterange EMPTY&gt;
    ///!ATTLIST daterange
    ///start     CDATA                  #REQUIRED
    ///stop      CDATA                  #REQUIRED
    ///quality   (estimated|calculated) #IMPLIED
    ///cformat   CDATA                  #IMPLIED
    ///dualdated (0|1)                  #IMPLIED
    ///newyear   CDATA                  #IMPLIED
    ///
    ///
    ///!ELEMENT datespan EMPTY&gt;
    ///!ATTLIST datespan
    ///start     CDATA                  #REQUIRED
    ///stop      CDATA                  #REQUIRED
    ///quality   (estimated|calculated) #IMPLIED
    ///cformat   CDATA                  #IMPLIED
    ///dualdated (0|1)                  #IMPLIED
    ///newyear   CDATA                  #IMPLIED
    ///
    ///
    ///!ELEMENT dateval EMPTY&gt;
    ///!ATTLIST dateval
    ///val       CDATA                  #REQUIRED
    ///type      (before|after|about)   #IMPLIED
    ///quality   (estimated|calculated) #IMPLIED
    ///cformat   CDATA                  #IMPLIED
    ///dualdated (0|1)                  #IMPLIED
    ///newyear   CDATA                  #IMPLIED
    ///&gt;
    ///
    ///!ELEMENT datestr EMPTY&gt;
    ///!ATTLIST datestr val CDATA #REQUIRED&gt;
    /// </code>
    /// </summary>

    public abstract class DateObjectModel : ModelBase, IDateObjectModel
    {
        /// <summary>
        /// Notional Date field - The date used for sorting etc.
        /// </summary>
        private DateTime _NotionalDate = DateTime.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateObjectModel"/> class.
        /// </summary>
        public DateObjectModel()
        {
        }

        /// <summary>
        /// Gets the number of years ago Because the field can have one or two dates etc this is
        /// trickier than it sounds.
        /// </summary>
        /// <returns>
        /// age.
        /// </returns>
        public abstract int GetAge
        {
            get;
        }

        /// <summary>
        /// Gets the decade of the date.
        /// </summary>
        /// <value>
        /// The get decade.
        /// </value>
        public int GetDecade
        {
            get
            {
                if (!Valid)
                {
                    return 0;
                }

                return ((int)Math.Floor(NotionalDate.Year / 10.0)) * 10;
            }
        }

        public DateTime GetMonthDay
        {
            get
            {
                if (!Valid)
                {
                    return DateTime.MinValue;
                }

                return new DateTime(DateTime.MinValue.Year, NotionalDate.Month, NotionalDate.Day);
            }
        }

        /// <summary>
        /// Gets the year of the date.
        /// </summary>
        /// <value>
        /// The date year.
        /// </value>
        public abstract string GetYear
        {
            get;
        }

        /// <summary>
        /// Gets the get long date as string. Default so it can be overridden.
        /// </summary>
        /// <value>
        /// The get long date as string.
        /// </value>
        public abstract string LongDate
        {
            get;
        }

        /// <summary>
        /// Gets the default Date field.
        /// </summary>
        [DataMember]
        public DateTime NotionalDate
        {
            get
            {
                return _NotionalDate;
            }

            internal set
            {
                SetProperty(ref _NotionalDate, value);
            }
        }

        /// <summary>
        /// Gets the string version of the date field. Default so it can be overridden.
        /// </summary>
        /// <returns>
        /// a string version of the date.
        /// </returns>
        public abstract string ShortDate
        {
            get;
        }

        public string ShortDateOrEmpty
        {
            get
            {
                if (!Valid)
                {
                    return String.Empty;
                }

                return ShortDate;
            }
        }

        /// <summary>
        /// Gets returns a single dateversion of the date field Because the field can have one or
        /// two dates etc this is trickier than it sounds. Overridden by more specific date types.
        /// </summary>
        /// <value>
        /// The single date.
        /// </value>
        public abstract DateTime SingleDate
        {
            get;
   
        }

        /// <summary>
        /// Gets returns a sortable version of the date field Because the field can have one or two
        /// dates etc this is trickier than it sounds. Overridden by more specific date types.
        /// </summary>
        /// <returns>
        /// A DateTime field that can be sorted.
        /// </returns>
        public abstract DateTime SortDate
        {
            get;
         
        }


      
        /// <summary>
        /// Gets or sets a value indicating whether the date is valid.
        /// </summary>
        /// <value>
        /// <c>true</c> if [date valid]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public new bool Valid { get; set; } = false;

        [DataMember]
        public bool ValidDay { get; set; } = false;

        [DataMember]
        public bool ValidMonth { get; set; } = false;

        [DataMember]
        public bool ValidYear { get; set; } = false;

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(DateObjectModel left, DateObjectModel right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(DateObjectModel left, DateObjectModel right)
        {
            return left is null ? right is object : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <=(DateObjectModel left, DateObjectModel right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(DateObjectModel left, DateObjectModel right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(DateObjectModel left, DateObjectModel right)
        {
            return left is object && left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >=(DateObjectModel left, DateObjectModel right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        public abstract CardListLineCollection AsCardListLine(string argTitle = null);

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to,
        /// or greater than the other.
        /// </summary>
        /// <param name="x">
        /// The first object to compare.
        /// </param>
        /// <param name="y">
        /// The second object to compare.
        /// </param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x"/> and
        /// <paramref name="y"/>, as shown in the following table. Value Meaning Less than zero
        /// <paramref name="x"/> is less than <paramref name="y"/>. Zero <paramref name="x"/> equals
        /// <paramref name="y"/>. Greater than zero <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        public int Compare(DateObjectModel x, DateObjectModel y)
        {
            if ((x is null) || (y is null))
            {
                return 1; // this is bigger
            }

            return DateTime.Compare(x.SortDate, y.SortDate);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an
        /// integer that indicates whether the current instance precedes, follows, or occurs in the
        /// same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">
        /// An object to compare with this instance.
        /// </param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return
        /// value has these meanings: Value Meaning Less than zero This instance precedes <paramref
        /// name="other"/> in the sort order. Zero This instance occurs in the same position in the
        /// sort order as <paramref name="other"/>. Greater than zero This instance follows
        /// <paramref name="other"/> in the sort order.
        /// </returns>
        public int CompareTo(DateObjectModel other)
        {
            Contract.Requires(other != null);

            return DateTime.Compare(SortDate, other.SortDate);
        }

        public int CompareTo(IDateObjectModel other)
        {
            Contract.Requires(other != null);

            return DateTime.Compare(SortDate, other.SortDate);
        }

        /// <summary>
        /// Dates the difference.
        /// </summary>
        /// <param name="otherDate">
        /// The other date.
        /// </param>
        /// <returns>
        /// </returns>
        public TimeSpan DateDifference(IDateObjectModel otherDate)
        {
            Contract.Requires(otherDate != null);

            if (Valid)
            {
                return SingleDate.Subtract(otherDate.SingleDate);
            }

            return new TimeSpan();
        }

        /// <summary>
        /// Dates the difference decoded.
        /// </summary>
        /// <param name="otherDate">
        /// The other date.
        /// </param>
        /// <returns>
        /// </returns>
        public string DateDifferenceDecoded(IDateObjectModel otherDate)
        {
            if (Valid)
            {
                // Because we start at year 1 for the Gregorian calendar, we must subtract a year here.
                DateTime zeroTime = new DateTime(1, 1, 1);
                int years = (zeroTime + DateDifference(otherDate)).Year - 1;

                // 1, where my other algorithm resulted in 0.
                return years + " years";
            }

            return "Unknown";
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
            return this.NotionalDate.GetHashCode();
        }

        /// <summary>
        /// Converts the RFC1123 string to date time.
        /// </summary>
        /// <param name="inputArg">
        /// The input argument.
        /// </param>
        /// <returns>
        /// </returns>
        internal DateTime ConvertRFC1123StringToDateTime(string inputArg)
        {
            // Try progressivly looser conversions

            // YYYY-MM-DD
            if (DateTime.TryParse(inputArg, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime outputDateTime) == true)
            {
                Valid = true;
                ValidYear = true;
                ValidMonth = true;
                ValidDay = true;
                return outputDateTime;
            }

            // YYYY-MM
            if (DateTime.TryParseExact(inputArg, "yyyy-MM", null, DateTimeStyles.None, out outputDateTime) == true)
            {
                Valid = true;
                ValidYear = true;
                ValidMonth = true;
                return outputDateTime;
            }

            // YYYY
            if (DateTime.TryParseExact(inputArg, "yyyy", null, DateTimeStyles.None, out outputDateTime) == true)
            {
                Valid = true;
                ValidYear = true;
                return outputDateTime;
            }

            // return null date

            return DateTime.MinValue;
        }
    }
}