// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Date;
using GrampsView.Models.HLinks.Models;

using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.ModelsDB.Date
{
    /// <summary>
    /// Create Str version of DateObjectModel.
    /// </summary>
    /// TODO Update fields as per Schema

    //[JsonDerivedType(typeof(DateObjectModelStr), typeDiscriminator: "str")]
    public class DateDBModelStr : DateDBModelBase, IDateDBModelStr
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateObjectModelStr"/> class. Date but is
        /// stored as a string so can not be converted to a DateTime.
        /// </summary>
        /// <param name="aVal">
        /// a value.
        /// </param>
        public DateDBModelStr(string aVal)
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

            DateType = DateObjectModelDerivedTypeEnum.DateObjectModelStr;

            Valid = true;
        }

        public DateDBModelStr()
        {
        }

        /// <summary>
        /// Not a properly formatted date so return null;
        /// </summary>
        public override int? GetAge => null;

        public override string GetYear => Valid ? GVal : "Unknown";

        [JsonInclude]
        public string GVal { get; set; } = string.Empty;


        /// <summary>
        /// Gets the $$(val)$$ field.
        /// </summary>
        [JsonIgnore]
        public HLinkDateDBModelStr HLink
        {
            get
            {
                HLinkDateDBModelStr t = new()
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

        public override HLinkDateDBModelStr AsHLink(string argTitle)
        {
            HLinkDateDBModelStr t = HLink;
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

            DateObjectModelBase? tempObj = obj as DateObjectModelBase;

            return NotionalDate == tempObj.NotionalDate;
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}