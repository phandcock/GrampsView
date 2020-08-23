//-----------------------------------------------------------------------
//
// The data model defined by this file serves to hold Event data from the GRAMPS data file
//
// <copyright file="DateObjectModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using GrampsView.Data.Repository;

    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// data model for an Date object ************************************************************.
    /// <code>
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
    [DataContract]
    public partial class DateObjectModel : ModelBase, IDateObjectModel
    {
        /// <summary>
        /// $$(cformat)$$ field.
        /// </summary>
        private string _GCformat = string.Empty;

        /// <summary>
        /// Dual dated field.
        /// </summary>
        private bool _GDualdated = false;

        /// <summary>
        /// New year field.
        /// </summary>
        private string _GNewYear = string.Empty;

        /// <summary>
        /// Quality field.
        /// </summary>
        private string _GQuality = string.Empty;

        /// <summary>
        /// Start field.
        /// </summary>
        private string _GStart = string.Empty;

        /// <summary>
        /// Stop field.
        /// </summary>
        private string _GStop = string.Empty;

        /// <summary>
        /// Type field.
        /// </summary>
        private DateType _GType = DateType.Unknown;

        /// <summary>
        /// $$(val)$$ field.
        /// </summary>
        private string _GVal = string.Empty;

        private string _GValType;

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
        /// Initializes a new instance of the <see cref="DateObjectModel"/> class.
        /// </summary>
        /// <param name="aDateType">
        /// Type of Date
        /// </param>
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
        /// <param name="aValType">
        /// Type of Val date.
        /// </param>
        public DateObjectModel(DateType aDateType, string aCFormat, bool aDualDated, string aNewYear, string aQuality, string aStart, string aStop, string aVal, string aValType)
        {
            // Setup defaults
            GCformat = aCFormat;
            GDualdated = aDualDated;
            GNewYear = aNewYear;
            GQuality = aQuality;
            GStart = aStart;
            GStop = aStop;
            GVal = aVal;
            GType = aDateType;
            GValType = aValType;

            // Setup specifics
            switch (aDateType)
            {
                case DateType.Range:
                    {
                        DateObjectModelRange(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop, aVal);
                        break;
                    }

                case DateType.Span:
                    {
                        DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop, aVal);
                        break;
                    }

                case DateType.Str:
                    {
                        DateObjectModelStr(aVal);
                        break;
                    }

                case DateType.Val:
                    {
                        DateObjectModelVal(aValType);
                        break;
                    }

                default:
                    {
                        DataStore.CN.NotifyError("Bad DateEnum: " + aDateType.ToString());
                        break;
                    }
            }
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

        /// <summary>
        /// Gets the number of years ago Because the field can have one or two dates etc this is
        /// trickier than it sounds.
        /// </summary>
        /// <returns>
        /// age.
        /// </returns>
        public int GetAge
        {
            get
            {
                if (!Valid)
                {
                    return 0;
                }

                switch (GType)
                {
                    case DateType.Range:
                        {
                            return RangeGetAge;
                        }

                    case DateType.Span:
                        {
                            return SpanGetAge;
                        }

                    case DateType.Str:
                        {
                            return StrGetAge;
                        }

                    case DateType.Val:
                        {
                            return ValGetAge;
                        }

                    default:
                        {
                            DataStore.CN.NotifyError("Bad DateEnum: " + GType.ToString());
                            return 0;
                        }
                }
            }
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

        public string GetMonthDay
        {
            get
            {
                if (!Valid)
                {
                    return "Unknown";
                }

                return NotionalDate.ToString("mmdd");
            }
        }

        /// <summary>
        /// Gets the year of the date.
        /// </summary>
        /// <value>
        /// The date year.
        /// </value>
        public string GetYear
        {
            get
            {
                if (!Valid)
                {
                    return "Unknown";
                }

                switch (GType)
                {
                    case DateType.Range:
                        {
                            return RangeGetYear;
                        }

                    case DateType.Span:
                        {
                            return SpanGetYear;
                        }

                    case DateType.Str:
                        {
                            return StrGetYear;
                        }

                    case DateType.Val:
                        {
                            return ValGetYear;
                        }

                    default:
                        {
                            DataStore.CN.NotifyError("Bad DateEnum: " + GType.ToString());
                            return "Unknonw";
                        }
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
        public string GQuality
        {
            get
            {
                return _GQuality;
            }

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _GQuality, value);
                }
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

        /// <summary>
        /// Gets the Date Type field.
        /// </summary>
        [DataMember]
        public DateType GType
        {
            get
            {
                return _GType;
            }

            private set
            {
                SetProperty(ref _GType, value);
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
        public string GValType
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

        /// <summary>
        /// Gets the get long date as string. Default so it can be overridden.
        /// </summary>
        /// <value>
        /// The get long date as string.
        /// </value>
        public string LongDate
        {
            get
            {
                if (!Valid)
                {
                    return "Unknown";
                }

                switch (GType)
                {
                    case DateType.Range:
                        {
                            return RangeGetLongDateAsString;
                        }

                    case DateType.Span:
                        {
                            return SpanGetLongDateAsString;
                        }

                    case DateType.Str:
                        {
                            return StrGetLongDateAsString;
                        }

                    case DateType.Val:
                        {
                            return ValGetLongDateAsString;
                        }

                    default:
                        {
                            DataStore.CN.NotifyError("Bad DateEnum: " + GType.ToString());
                            return "Unknown";
                        }
                }
            }
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
        public string ShortDate
        {
            get
            {
                if (!Valid)
                {
                    return "Unknown";
                }

                switch (GType)
                {
                    case DateType.Range:
                        {
                            return RangeGetShortDateAsString;
                        }

                    case DateType.Span:
                        {
                            return SpanGetShortDateAsString;
                        }

                    case DateType.Str:
                        {
                            return StrGetShortDateAsString;
                        }

                    case DateType.Val:
                        {
                            return ValGetShortDateAsString;
                        }

                    default:
                        {
                            DataStore.CN.NotifyError("Bad DateEnum: " + GType.ToString());
                            return "Unknown";
                        }
                }
            }
        }

        ///// <summary>
        ///// Gets an empty string if a null date or the date string. Used for formatting. Default so
        ///// it can be overridden.
        ///// </summary>
        //public string GetShortDateOrEmptyAsString
        //{
        //    get
        //    {
        //        if (!Valid)
        //        {
        //            return "Unknown";
        //        }

        //        return ShortDate;
        //    }
        //}

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
        public virtual DateTime SingleDate
        {
            get
            {
                // DateTime outputDateTime;

                // switch (GType) { case DateType.daterange: { if (DateTime.TryParse(Start, out
                // outputDateTime) == false) { outputDateTime = DateTime.MinValue; }

                // break; }

                // case DateType.datespan: { if (DateTime.TryParse(Start, out outputDateTime) ==
                // false) { outputDateTime = DateTime.MinValue; }

                // break; }

                // case DateType.dateval: { outputDateTime = notionalDateField; break; }

                // case DateType.datestr: { outputDateTime = notionalDateField; break; }

                // case DateType.nullDate: { outputDateTime = DateTime.MinValue; break; }

                // default: { outputDateTime = DateTime.MinValue; break; } }
                return NotionalDate;
            }
        }

        //public DateTimeFormatter ShortDate
        //{
        //    get
        //    {
        //        return new DateTimeFormatter("‎{day.integer} {month.abbreviated} ‎{year.full}");
        //    }
        //}
        // private set { SetProperty(ref GTypeField, value); } }
        /// <summary>
        /// Gets returns a sortable version of the date field Because the field can have one or two
        /// dates etc this is trickier than it sounds. Overridden by more specific date types.
        /// </summary>
        /// <returns>
        /// A DateTime field that can be sorted.
        /// </returns>
        public virtual DateTime SortDate
        {
            get
            {
                // DateTime outputDateTime;

                // switch (GType) { case DateType.daterange: { if (DateTime.TryParse(Start,
                // CultureInfo.InvariantCulture, DateTimeStyles.None, out
                // outputDateTime) == false) { outputDateTime = DateTime.MinValue; }

                // break; }

                // case DateType.datespan: { if (DateTime.TryParse(Start, out outputDateTime) ==
                // false) { outputDateTime = DateTime.MinValue; }

                // break; }

                // case DateType.dateval: { outputDateTime = notionalDateField; break; }

                // case DateType.datestr: { outputDateTime = notionalDateField; break; }

                // case DateType.nullDate: { outputDateTime = DateTime.MinValue; break; }

                // default: { outputDateTime = DateTime.MinValue; break; } }
                return NotionalDate;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the date is valid.
        /// </summary>
        /// <value>
        /// <c>true</c> if [date valid]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public new bool Valid { get; set; } = false;

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
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
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
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
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
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
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
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
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
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }

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
            return DateTime.Compare(SortDate, other.SortDate);
        }

        public int CompareTo(IDateObjectModel other)
        {
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

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            DateObjectModel tempObj = obj as DateObjectModel;

            return ((this.GType == tempObj.GType) && (this.NotionalDate == tempObj.NotionalDate));
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
            // Default to the Minimum DateTime
            DateTime outputDateTime;

            // try progressivly looser conversions

            // YYYY-MM-DD
            if (DateTime.TryParse(inputArg, CultureInfo.InvariantCulture, DateTimeStyles.None, out outputDateTime) == true)
            {
                Valid = true;
                return outputDateTime;
            }

            // YYYY-MM
            if (DateTime.TryParseExact(inputArg, "yyyy-MM", null, DateTimeStyles.None, out outputDateTime) == true)
            {
                Valid = true;
                return outputDateTime;
            }

            // YYYY
            if (DateTime.TryParseExact(inputArg, "yyyy", null, DateTimeStyles.None, out outputDateTime) == true)
            {
                Valid = true;
                return outputDateTime;
            }

            // return null date

            return DateTime.MinValue;
        }
    }
}