﻿/// <summary>
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

                if (GMother.Valid)
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

                if (GMother.Valid)
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

        //public string FamilyRelationshipWithPrefix
        //{
        //    get
        //    {
        //        if (Valid & (!string.IsNullOrWhiteSpace(GFamilyRelationship)))
        //        {
        //            return $"{AppResources.FieldPrefixFamilyRelationship} {GFamilyRelationship}";
        //        }

        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// Gets or sets the g attribute collection. This is the [attribute*] attribute.
        /// </summary>
        /// <value>
        /// The g attribute collection.
        /// </value>
        [DataMember]
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
        [DataMember]
        public ChildRefCollectionCollection GChildRefCollection
        {
            get;

            set;
        }

        = new ChildRefCollectionCollection();

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get;

            set;
        } = new HLinkCitationModelCollection();

        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
        [DataMember]
        public HLinkPersonModel GFather
        {
            get;

            set;
        } = new HLinkPersonModel();

        [DataMember]
        public OCLdsOrdModelCollection GLDSOrdCollection
        {
            get;

            set;
        } = new OCLdsOrdModelCollection();

        /// <summary>
        /// Gets or sets the g media reference collection. This is the [objref*] attribute.
        /// </summary>
        /// <value>
        /// The g media reference collection.
        /// </value>
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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