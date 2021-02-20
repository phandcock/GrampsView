// TODO Needs XML 1.71 check

/// <summary>
///
///  -- Completed
///  - SecondaryColor-object
///  - rel
///  - father
///  - mother
///  - eventref
///  - objref
///  -
///  - attribute
///  -
///  - noteref
///  - citationref
///  - tagref
///
/// </summary>
///
/// TODO Finish adding these
///// <code>
////
////    <zeroOrMore>
////      <element name = "childref" >
////        < attribute name="hlink">
////          <data type = "IDREF" />
////        </ attribute >
////        < optional >
////          < attribute name="priv">
////            <ref name="priv-content" />
////          </attribute>
////        </optional>
////        <optional>
////          <attribute name = "mrel" >
////            <ref name="child-rel" />
////          </attribute>
////        </optional>
////        <optional>
////          <attribute name = "frel" >
////            <ref name="child-rel" />
////          </attribute>
////        </optional>
////        <zeroOrMore>
////          <element name = "citationref" >
////            <ref name="citationref-content" />
////          </element>
////        </zeroOrMore>
////        <zeroOrMore>
////          <element name = "noteref" >
////            <ref name="noteref-content" />
////          </element>
////        </zeroOrMore>
////      </element>
////    </zeroOrMore>
////
////    <optional>
////      <ref name="date-content" />
////    </optional>
///// </code>
/// TODO Update fields as per Schema
namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;
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
    [DataContract]
    [KnownType(typeof(HLinkPersonModel))]
    public sealed class FamilyModel : ModelBase, IFamilyModel, IComparable, IComparer
    {
        /// <summary>
        /// The local attribute reference.
        /// </summary>
        private OCAttributeModelCollection _AttributeReference = new OCAttributeModelCollection();

        // citationref*
        private HLinkCitationModelCollection _CitationReferenceCollection = new HLinkCitationModelCollection();

        /// <summary>
        /// The local event collection.
        /// </summary>
        private HLinkEventModelCollection _EventReferenceCollection = new HLinkEventModelCollection();

        /// <summary>
        /// Backing store for the HLink Note collection.
        /// </summary>
        private HLinkNoteModelCollection _GNoteRefCollection = new HLinkNoteModelCollection();

        /// <summary>
        /// The local media collection.
        /// </summary>
        private HLinkMediaModelCollection _MediaReferenceCollection = new HLinkMediaModelCollection();

        /// <summary>
        /// Collection of Child References $$(childref)$$.
        /// </summary>
        private HLinkPersonModelCollection childRefCollection = new HLinkPersonModelCollection("Children");

        /// <summary>
        /// relationship $$(rel)$$.
        /// </summary>
        private string familyRelationshipField = string.Empty;

        /// <summary>
        /// Family Father Handle father?.
        /// </summary>
        private HLinkPersonModel fatherHLink = new HLinkPersonModel();

        /// <summary>
        /// Family Father Handle mother?.
        /// </summary>
        private HLinkPersonModel motherHLink = new HLinkPersonModel();

        //// TODO lds_ord*

        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyModel"/> class.
        /// </summary>
        public FamilyModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconFamilies;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundFamily");
        }

        /// <summary>
        /// Gets the Mother and Father display name.
        /// </summary>
        /// <value>
        /// The display name of the family.
        /// </value>
        public string FamilyDisplayName
        {
            get
            {
                StringBuilder familyName = new StringBuilder();

                string fatherName = GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname;
                string motherName = GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname;

                // set family display name
                if (GFather.Valid)
                {
                    familyName.Append(fatherName);
                }
                else
                {
                    familyName.Append("Unknown");
                }

                if (motherHLink.Valid)
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

        /// <summary>
        /// Gets the family display name sort.
        /// </summary>
        /// <value>
        /// The family display name sort.
        /// </value>
        public string FamilyDisplayNameSort
        {
            get
            {
                string familyName;

                // set family display name
                if (GFather.Valid)
                {
                    familyName = GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname;
                }
                else
                {
                    familyName = "Unknown";
                }

                if (motherHLink.Valid)
                {
                    StringBuilder t = new StringBuilder();
                    t.Append(familyName);
                    t.Append(GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname);
                    familyName = t.ToString();
                }
                else
                {
                    familyName += "Unknown";
                }

                return familyName;
            }
        }

        /// <summary>
        /// Gets or sets the g attribute collection. This is the [attribute*] attribute.
        /// </summary>
        /// <value>
        /// The g attribute collection.
        /// </value>
        [DataMember]
        public OCAttributeModelCollection GAttributeCollection
        {
            get
            {
                return _AttributeReference;
            }

            set
            {
                SetProperty(ref _AttributeReference, value);
            }
        }

        /// <summary>
        /// Gets or sets Child Reference collection.
        /// </summary>
        /// <value>
        /// The g child reference collection.
        /// </value>
        [DataMember]
        public HLinkPersonModelCollection GChildRefCollection
        {
            get
            {
                return childRefCollection;
            }

            set
            {
                SetProperty(ref childRefCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get
            {
                return _CitationReferenceCollection;
            }

            set
            {
                SetProperty(ref _CitationReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the event collection. This is the [eventref*] attribute.
        /// </summary>
        /// <value>
        /// The event collection.
        /// </value>
        [DataMember]
        public HLinkEventModelCollection GEventRefCollection
        {
            get
            {
                return _EventReferenceCollection;
            }

            set
            {
                SetProperty(ref _EventReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets Family Relationship.
        /// </summary>
        /// <value>
        /// The family relationship.
        /// </value>
        [DataMember]
        public string GFamilyRelationship
        {
            get
            {
                return familyRelationshipField;
            }

            set
            {
                SetProperty(ref familyRelationshipField, value);
            }
        }

        /// <summary>
        /// Gets or sets Fathers $$(hLink)$$.
        /// </summary>
        /// <value>
        /// The fathers h link.
        /// </value>
        [DataMember]
        public HLinkPersonModel GFather
        {
            get
            {
                return fatherHLink;
            }

            set
            {
                SetProperty(ref fatherHLink, value);
            }
        }

        /// <summary>
        /// Gets or sets the g media reference collection. This is the [objref*] attribute.
        /// </summary>
        /// <value>
        /// The g media reference collection.
        /// </value>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get
            {
                return _MediaReferenceCollection;
            }

            set
            {
                SetProperty(ref _MediaReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets Mothers $$(hLink)$$.
        /// </summary>
        /// <value>
        /// The mothers h link.
        /// </value>
        [DataMember]
        public HLinkPersonModel GMother
        {
            get
            {
                return motherHLink;
            }

            set
            {
                SetProperty(ref motherHLink, value);
            }
        }

        /// <summary>
        /// Gets or sets the HLink Note reference collection.
        /// </summary>
        /// <value>
        /// The g note reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get
            {
                return _GNoteRefCollection;
            }

            set
            {
                SetProperty(ref _GNoteRefCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the g tag reference collection. This is the [tagref*] attribute.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection
        {
            get; set;
        }

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

            // compare on surnname first
            int testFlag = string.Compare(c1.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName, c2.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName, StringComparison.CurrentCulture);

            if (testFlag.Equals(0))
            {
                // equal so check firstname
                testFlag = string.Compare(c1.GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName, c2.GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName, StringComparison.CurrentCulture);
            }

            return testFlag;
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
        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            FamilyModel secondFamilyModel = (FamilyModel)obj;

            // compare on fathers name first TODO use culture related sort
            int testFlag = string.Compare(GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName, secondFamilyModel.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName, StringComparison.CurrentCulture);

            if (testFlag.Equals(0))
            {
                // equal so check firstname
                testFlag = string.Compare(GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName, secondFamilyModel.GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName, StringComparison.CurrentCulture);
            }

            return testFlag;
        }
    }
}