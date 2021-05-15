namespace GrampsView.Data.Model
{
    using GrampsView.Data.Repository;

    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Create Str version of DateObjectModel.
    /// </summary>
    /// TODO Update fields as per Schema
    public class DateObjectModelStr : DateObjectModel, IDateObjectModelStr
    {
        /// <summary>
        /// $$(val)$$ field.
        /// </summary>
        private string _GVal = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateObjectModelStr"/> class. Date but
        /// stored as a string so can not be converted to a DateTime.
        /// </summary>
        /// <param name="aVal">
        /// a value.
        /// </param>
        public DateObjectModelStr(string aVal)
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
                DataStore.Instance.CN.NotifyException("Error in SetDate", e);
                throw;
            }
        }

        /// <summary>
        /// Not a properly formatted date so return 0;
        /// </summary>
        public override Nullable<int> GetAge
        {
            get
            {
                return null;
            }
        }

        public override string GetYear
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
                return GVal;
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
                                new CardListLine("Val:", this.GVal),
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