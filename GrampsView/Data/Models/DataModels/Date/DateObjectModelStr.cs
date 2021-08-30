namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System;
    using System.Diagnostics.Contracts;
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
        /// Initializes a new instance of the <see cref="DateObjectModelStr"/> class. Date but is
        /// stored as a string so can not be converted to a DateTime.
        /// </summary>
        /// <param name="aVal">
        /// a value.
        /// </param>
        public DateObjectModelStr(string aVal)
        {
            Contract.Requires(!String.IsNullOrEmpty(aVal));

            // Setup basics
            ModelItemGlyph.Symbol = CommonConstants.IconDate;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            HLinkKey = Common.CustomClasses.HLinkKey.NewAsGUID();

            GVal = aVal;

            // Set NotionalDate
            NotionalDate = DateTime.MinValue;

            Valid = true;
        }

        public DateObjectModelStr()
        {
        }

        /// <summary>
        /// Not a properly formatted date so return null;
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

        public HLinkDateModelStr HLink
        {
            get
            {
                HLinkDateModelStr t = new HLinkDateModelStr
                {
                    DeRef = this,
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
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

        public override CardListLineCollection AsCardListLine(string argTitle = "Date Detail")
        {
            CardListLineCollection DateModelCard = new CardListLineCollection();

            if (this.Valid)
            {
                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date:", this.LongDate),
                                new CardListLine("Str:", this.GVal),
                            };
            }

            if (!(string.IsNullOrEmpty(argTitle)))
            {
                DateModelCard.Title = argTitle;
            }

            return DateModelCard;
        }

        public override HLinkBase AsHLink(string argTitle)
        {
            HLinkDateModelStr t = this.HLink;
            t.Title = argTitle;

            return t;
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