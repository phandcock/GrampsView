// XML 1.71 check complete

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
        /// The g citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationReferenceCollection
        {
            get;
            set;
        }

            = new HLinkCitationModelCollection();

        public override string GetDefaultText
        {
            get
            {
                string returnString = this.GType;

                if (!string.IsNullOrEmpty(GValue))
                {
                    returnString += string.Format(" {0}", GValue);
                }

                return returnString;
            }
        }

        /// <summary>
        /// Gets or sets the note model reference collection.
        /// </summary>
        /// <value>
        /// The g note model reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteModelReferenceCollection
        {
            get;
            set;
        }

            = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the g text.
        /// </summary>
        /// <value>
        /// The g text.
        /// </value>
        [DataMember]
        public string GType
        {
            get;
            set;
        }

            = null;

        /// <summary>
        /// Gets or sets the g value.
        /// </summary>
        /// <value>
        /// The g value.
        /// </value>
        [DataMember]
        public string GValue
        {
            get;
            set;
        }

            = null;

        public override bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(GType);
            }
        }

        /// <summary>
        /// Compares the specified x.
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

            AttributeModel secondSource = (AttributeModel)obj;

            // compare on Page first TODO compare on Page?
            return string.Compare(GType, secondSource.GType, true, System.Globalization.CultureInfo.CurrentCulture);
        }

        public override async Task UCNavigate()
        {
            await UCNavigateBase(this, nameof(AttributeDetailPage));
            return;
        }
    }
}