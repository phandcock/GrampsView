namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Views;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Holds item specific attributes. XML 1.71 check complete
    /// </summary>
    [DataContract]
    public class AttributeModel : ModelBase, IAttributeModel, IComparable, IComparer<AttributeModel>
    {
        public AttributeModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconAttribute;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundAttribute");
        }

        /// <summary>
        /// Gets or sets the citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationReferenceCollection
        {
            get;
            set;
        }

            = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets the default text for this Model.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public override string GetDefaultText
        {
            get
            {
                string returnString = this.GType;

                if (!string.IsNullOrEmpty(GValue))
                {
                    returnString += $" {GValue}";
                }

                return returnString;
            }
        }

        /// <summary>
        /// Gets or sets the note model reference collection.
        /// </summary>
        /// <value>
        /// The note model reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteModelReferenceCollection
        {
            get;
            set;
        }

            = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the attribute type.
        /// </summary>
        /// <value>
        /// The attribute text.
        /// </value>
        [DataMember]
        public string GType
        {
            get;
            set;
        }

            = null;

        /// <summary>
        /// Gets or sets the attribute value.
        /// </summary>
        /// <value>
        /// The value text.
        /// </value>
        [DataMember]
        public string GValue
        {
            get;
            set;
        }

            = null;

        /// <summary>
        /// Gets a value indicating whether the modelbase is valid.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is valid; otherwise, <c>false</c>.
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
        /// Compares to.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            AttributeModel secondSource = obj as AttributeModel;

            // compare on GType first
            return string.Compare(GType, secondSource.GType, true, System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Navigates to the detail page for the attribute.
        /// </summary>
        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(AttributeDetailPage));
            return;
        }
    }
}