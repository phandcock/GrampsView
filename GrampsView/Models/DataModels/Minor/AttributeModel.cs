// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Models.Collections.HLinks;
    using GrampsView.Models.DataModels;
    using GrampsView.ModelsDB.Collections.HLinks;

    using SharedSharp.Common;

    using System;
    using System.Diagnostics.Contracts;
    using System.Text.Json.Serialization;

    /// <summary>
    /// <para> Holds details of Attributes. </para>
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Done </description>
    /// </item>
    /// </list>
    /// <para> <br/> </para>
    /// </summary>

    public class AttributeModel : ModelBase, IAttributeModel
    {
        public AttributeModel()
        {
            ModelItemGlyph.Symbol = Constants.IconAttribute;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAttribute");
        }

        /// <summary>
        /// Gets or sets the citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>

        public HLinkCitationDBModelCollection GCitationReferenceCollection
        {
            get;
            set;
        }

            = new HLinkCitationDBModelCollection();

        /// <summary>
        /// Gets or sets the note model reference collection.
        /// </summary>
        /// <value>
        /// The note model reference collection.
        /// </value>

        public HLinkNoteDBModelCollection GNoteModelReferenceCollection
        {
            get;
            set;
        }

            = new HLinkNoteDBModelCollection();

        /// <summary>
        /// Gets or sets the attribute type.
        /// </summary>
        /// <value>
        /// The attribute text.
        /// </value>

        public string GType
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// Gets or sets the attribute value.
        /// </summary>
        /// <value>
        /// The value text.
        /// </value>

        public string GValue
        {
            get;
            set;
        } = string.Empty;

        [JsonIgnore]
        public HLinkAttributeModel HLink
        {
            get
            {
                HLinkAttributeModel t = new HLinkAttributeModel
                {
                    DeRef = this,
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the modelbase is valid.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is valid; otherwise, <c> false </c>.
        /// </value>
        public override bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(GType);
            }
        }

        /// <summary>
        /// Compares the specified two attribute models.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// </returns>
        public int Compare(AttributeModel x, AttributeModel y)
        {
            Contract.Requires(x != null);

            return Compare(x.GType, y.GType);
        }

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="a">
        /// object A.
        /// </param>
        /// <param name="b">
        /// object B.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        public override int Compare(object a, object b)
        {
            if (a is null)
            {
                return SharedConstants.CompareEquals;
            }

            if (b is null)
            {
                return SharedConstants.CompareEquals;
            }

            AttributeModel firstAttributeName = (AttributeModel)a;
            AttributeModel secondAttributeName = (AttributeModel)b;

            // Compare on Surname first
            int testFlag = string.Compare(firstAttributeName.GType, secondAttributeName.GType, StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public override int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            AttributeModel secondSource = obj as AttributeModel;

            // compare on GType first
            return string.Compare(GType, secondSource.GType, true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public int CompareTo(AttributeModel other)
        {
            if (other is null)
            {
                return SharedConstants.CompareGreaterThan;
            }

            // This is effectively random
            return CompareTo(other);
        }

        /// <summary>
        /// Gets the default text for this Model.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public override string ToString()
        {
            string returnString = GType;

            if (!string.IsNullOrEmpty(GValue))
            {
                returnString += $" {GValue}";
            }

            return returnString;
        }
    }
}