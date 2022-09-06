/// <summary>
/// -- Completed
/// - SecondaryColor-object
/// - rel
/// - father
/// - mother
/// - date
/// - eventref
/// - ldsorf
/// - objref
/// - childref
/// - attribute
/// - noteref
/// - citationref
/// - tagref
/// </summary>

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// Data model for a family.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.ViewModel.IFamilyModel"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.IComparable"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.Collections.IComparer"/>

    [KnownType(typeof(HLinkPersonModel))]
    public sealed class FamilyModel : ModelBase, IFamilyModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyModel"/> class.
        /// </summary>
        public FamilyModel()
        {
            ModelItemGlyph.Symbol = Constants.IconFamilies;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundFamily");
        }

        /// <summary>
        /// Gets or sets the g attribute collection. This is the [attribute*] attribute.
        /// </summary>
        /// <value>
        /// The g attribute collection.
        /// </value>

        public HLinkAttributeModelCollection GAttributeCollection
        {
            get;

            set;
        } = new HLinkAttributeModelCollection();

        /// <summary>
        /// Gets or sets Child Reference collection.
        /// </summary>
        /// <value>
        /// The g child reference collection.
        /// </value>

        public HLinkChildRefCollection GChildRefCollection
        {
            get;

            set;
        }

        = new HLinkChildRefCollection();

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>

        public HLinkCitationModelCollection GCitationRefCollection
        {
            get;

            set;
        } = new HLinkCitationModelCollection();

        public DateObjectModel GDate
        {
            get; set;
        } = new DateObjectModelVal();

        /// <summary>
        /// Gets or sets the event collection. This is the [eventref*] attribute.
        /// </summary>
        /// <value>
        /// The event collection.
        /// </value>

        public HLinkEventModelCollection GEventRefCollection
        {
            get; set;
        } = new HLinkEventModelCollection();

        /// <summary>
        /// Gets or sets Family Relationship.
        /// </summary>
        /// <value>
        /// The family relationship.
        /// </value>

        public string GFamilyRelationship
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets Fathers $$(hLink)$$.
        /// </summary>
        /// <value>
        /// The fathers h link.
        /// </value>

        public HLinkPersonModel GFather
        {
            get;

            set;
        } = new HLinkPersonModel();

        public HLinkLdsOrdModelCollection GLDSOrdCollection
        {
            get;

            set;
        } = new HLinkLdsOrdModelCollection();

        /// <summary>
        /// Gets or sets the g media reference collection. This is the [objref*] attribute.
        /// </summary>
        /// <value>
        /// The g media reference collection.
        /// </value>

        public HLinkMediaModelCollection GMediaRefCollection
        {
            get;

            set;
        } = new HLinkMediaModelCollection();

        /// <summary>
        /// Gets or sets Mothers $$(hLink)$$.
        /// </summary>
        /// <value>
        /// The mothers h link.
        /// </value>

        public HLinkPersonModel GMother
        {
            get;

            set;
        } = new HLinkPersonModel();

        /// <summary>
        /// Gets or sets the HLink Note reference collection.
        /// </summary>
        /// <value>
        /// The g note reference collection.
        /// </value>

        public HLinkNoteModelCollection GNoteRefCollection
        {
            get;

            set;
        } = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the g tag reference collection. This is the [tagref*] attribute.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>

        public HLinkTagModelCollection GTagRefCollection
        {
            get; set;
        } = new HLinkTagModelCollection();

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkFamilyModel HLink
        {
            get
            {
                HLinkFamilyModel t = new HLinkFamilyModel
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };
                return t;
            }
        }

        /// <summary>
        /// Compare two FamilyModels.
        /// </summary>
        /// <param name="x">
        /// first object.
        /// </param>
        /// <param name="y">
        /// second object.
        /// </param>
        /// <returns>
        /// returns 1, 2, or 3.
        /// </returns>
        public new int Compare(object x, object y)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y is null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            FamilyModel c1 = (FamilyModel)x;
            FamilyModel c2 = (FamilyModel)y;

            // Compare on surnname and then first name
            return c1.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.CompareTo(c2.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef);
        }

        public int CompareTo(FamilyModel argSecondFamilyModel)
        {
            if (argSecondFamilyModel is null)
            {
                throw new ArgumentNullException(nameof(argSecondFamilyModel));
            }

            // Compare on surnname and then first name
            return GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.CompareTo(argSecondFamilyModel.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef);
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// compare to object.
        /// </param>
        /// <returns>
        /// returns 1, 2 or 3.
        /// </returns>
        public new int CompareTo(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            FamilyModel secondFamilyModel = (FamilyModel)obj;

            // Compare on surnname and then first name
            return this.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.CompareTo(secondFamilyModel.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef);
        }

        /// <summary>
        /// Gets the Mother and Father display name.
        /// </summary>
        /// <value>
        /// The display name of the family.
        /// </value>
        public override string ToString()
        {
            StringBuilder familyName = new StringBuilder();

            string fatherName = GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname;
            string motherName = GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname;

            // set family display name
            if (GFather.Valid && !string.IsNullOrWhiteSpace(fatherName))
            {
                familyName.Append(fatherName);
            }
            else
            {
                familyName.Append("Unknown");
            }

            if (GMother.Valid && !string.IsNullOrWhiteSpace(motherName))
            {
                familyName.Append(" - ");
                familyName.Append(motherName);
            }
            else
            {
                familyName.Append(" - Unknown");
            }

            return familyName.ToString();
        }
    }
}