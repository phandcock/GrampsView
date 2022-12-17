using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.HLinks;
using GrampsView.Models.HLinks.Models;

using SharedSharp.Model;
using SharedSharp.Models;

using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;


/* Unmerged change from project 'GrampsView (net7.0-windows10.0.19041.0)'
Before:
namespace GrampsView.Data.Model
After:
namespace GrampsView.Models.DataModels.Date
*/
namespace GrampsView.Models.DataModels.Date
{
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
            Contract.Requires(!string.IsNullOrEmpty(aVal));

            // Setup basics
            ModelItemGlyph.Symbol = Constants.IconDate;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
            //DerivedType = CommonEnums.DateObjectModelDerivedTypeEnum.DateObjectModelStr;

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
        public override int? GetAge => null;

        public override string GetYear => Valid ? GVal : "Unknown";

        public string GVal
        {
            get => _GVal;

            internal set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _ = SetProperty(ref _GVal, value);
                }
            }
        }

        /// <summary>
        /// Gets the $$(val)$$ field.
        /// </summary>
        [JsonIgnore]
        public HLinkDateModelStr HLink
        {
            get
            {
                HLinkDateModelStr t = new()
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
        public override string LongDate => !Valid ? string.Empty : GVal;

        /// <summary>
        /// Gets the string version of the date field.
        /// </summary>
        /// <returns>
        /// a string version of the date.
        /// </returns>
        public override string ShortDate => !Valid ? string.Empty : GVal;

        public override CardListLineCollection AsCardListLine(string argTitle = "Date Detail")
        {
            CardListLineCollection DateModelCard = new();

            if (Valid)
            {
                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date:", LongDate),
                                new CardListLine("Str:", GVal),
                            };
            }

            if (!string.IsNullOrEmpty(argTitle))
            {
                DateModelCard.Title = argTitle;
            }

            return DateModelCard;
        }

        public override HLinkBase AsHLink(string argTitle)
        {
            HLinkDateModelStr t = HLink;
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

            if (GetType() != obj.GetType())
            {
                return false;
            }

            DateObjectModel? tempObj = obj as DateObjectModel;

            return NotionalDate == tempObj.NotionalDate;
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}