//-----------------------------------------------------------------------
//
// Storage model for the PersonModel
//
// <copyright file="PersonModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////    -- All Completed (Version 1.71)
////    - address
////    - attribute
////    - childof
////    - citationref
////    - eventref
////    - gender
////    - lds_ord
////    - name
////    - noteref
////    - parentin
////    - personref
////    - primary-object
////    - tagref
////    - url
////

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    /// <summary>
    /// Data model for a person.
    /// </summary>
    [DataContract]
    [KnownType(typeof(HLinkFamilyModel))]
    public sealed class PersonModel : ModelBase, IPersonModel, IComparable, IComparer
    {
        /// <summary>
        /// The local attribute reference.
        /// </summary>
        private OCAttributeModelCollection _AttributeReference = new OCAttributeModelCollection();

        /// <summary>
        /// Reference to the parent family object.
        /// </summary>
        private HLinkFamilyModel _ChildOf = new HLinkFamilyModel();

        /// <summary>
        /// The local citation reference.
        /// </summary>
        private HLinkCitationModelCollection _CitationReference = new HLinkCitationModelCollection();

        /// <summary>
        /// Person Element - Event References.
        /// </summary>
        private HLinkEventModelCollection _EventReference = new HLinkEventModelCollection();

        /// <summary>
        /// Person Element - Gender.
        /// </summary>
        private string _Gender = string.Empty;

        /// <summary>
        /// The local LDS collection.
        /// </summary>
        private OCLdsOrdModelCollection _GLDSCollection = new OCLdsOrdModelCollection();

        /// <summary>
        /// Person Element - Name.
        /// </summary>
        private HLinkPersonNameModelCollection _GPersonNamesCollection = new HLinkPersonNameModelCollection();

        /// <summary>
        /// The local is living.
        /// </summary>
        private bool _IsLiving;

        /// <summary>
        /// Collection of Media References $$(mediaRef)$$.
        /// </summary>
        private HLinkMediaModelCollection _MediaCollection = new HLinkMediaModelCollection();

        /// <summary>
        /// Person Element - Note References.
        /// </summary>
        private HLinkNoteModelCollection _NoteReference = new HLinkNoteModelCollection();

        /// <summary>
        /// Collection of Family References $$(parentin)$$.
        /// </summary>
        private HLinkFamilyModelCollection _ParentInCollection = new HLinkFamilyModelCollection();

        /// <summary>
        /// The local sibling reference collection.
        /// </summary>
        private HLinkPersonModelCollection _SiblingRefCollection = new HLinkPersonModelCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        public PersonModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconPeople;
            HomeImageHLink.HomeSymbolColour = HomeImageHLink.HomeSymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [DataMember]
        public IDateObjectModel BirthDate
        {
            get; set;
        }

            = new DateObjectModelVal();

        /// <summary>
        /// Gets or sets address collection.
        /// </summary>
        [DataMember]
        public HLinkOCAddressModelCollection GAddress { get; set; }

        = new HLinkOCAddressModelCollection();

        /// <summary>
        /// Gets or sets the g attribute collection.
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
        /// Gets or sets the child of.
        /// </summary>
        /// <value>
        /// The child of.
        /// </value>
        [DataMember]
        public HLinkFamilyModel GChildOf
        {
            get
            {
                return _ChildOf;
            }

            set
            {
                SetProperty(ref _ChildOf, value);
            }
        }

        /// <summary>
        /// Gets or sets the citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get
            {
                return _CitationReference;
            }

            set
            {
                SetProperty(ref _CitationReference, value);
            }
        }

        /// <summary>
        /// Gets the get default text for this ViewModel.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public override string GetDefaultText
        {
            get
            {
                return GPersonNamesCollection.GetPrimaryName.DeRef.GetDefaultText;
            }
        }

        /// <summary> Gets or sets the Event Reference Collection.
        // </summary>
        [DataMember]
        public HLinkEventModelCollection GEventRefCollection
        {
            get
            {
                return _EventReference;
            }

            set
            {
                SetProperty(ref _EventReference, value);
            }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [DataMember]
        public string GGender
        {
            get
            {
                return _Gender;
            }

            set
            {
                SetProperty(ref _Gender, value);

                //switch (value)
                //{
                //    case "F":
                //        {
                //            HomeImageHLink.HomeSymbol = IconFont.GenderFemale;
                //            break;
                //        }

                // case "M": { HomeImageHLink.HomeSymbol = IconFont.GenderMale; break; }

                // case "U": { HomeImageHLink.HomeSymbol = IconFont.BatteryUnknown; break; }

                //    default:
                //        HomeImageHLink.HomeSymbol = IconFont.BatteryUnknown;
                //        break;
                //}
            }
        }

        /// <summary>
        /// Gets the gender decode.
        /// </summary>
        /// <value>
        /// The gender decode.
        /// </value>
        public string GGenderAsString
        {
            get
            {
                switch (GGender)
                {
                    case "F":
                        {
                            return "Female";
                        }

                    case "M":
                        {
                            return "Male";
                        }

                    case "U":
                        {
                            return "Unknown";
                        }

                    default:
                        return "Unknown";
                }
            }
        }

        /// <summary>
        /// Gets or sets the LDS collection.
        /// </summary>
        /// <value>
        /// The GLDS collection.
        /// </value>
        [DataMember]
        public OCLdsOrdModelCollection GLDSCollection
        {
            get
            {
                return _GLDSCollection;
            }

            set
            {
                SetProperty(ref _GLDSCollection, value);
            }
        }

        /// <summary> Gets or sets Media In $$(hLink)$$.
        // </summary>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get
            {
                return _MediaCollection;
            }

            set
            {
                SetProperty(ref _MediaCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the Note reference collection.
        /// </summary>
        /// <value>
        /// The Note reference.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get
            {
                return _NoteReference;
            }

            set
            {
                SetProperty(ref _NoteReference, value);
            }
        }

        /// <summary>
        /// Gets or sets the parent relationship collection.
        /// </summary>
        [DataMember]
        public HLinkFamilyModelCollection GParentInRefCollection
        {
            get
            {
                return _ParentInCollection;
            }

            set
            {
                SetProperty(ref _ParentInCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the Persons Birthname.
        /// </summary>
        [DataMember]
        public HLinkPersonNameModelCollection GPersonNamesCollection
        {
            get
            {
                return _GPersonNamesCollection;
            }

            set
            {
                SetProperty(ref _GPersonNamesCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the g person reference collection.
        /// </summary>
        /// <value>
        /// The g person reference collection.
        /// </value>
        [DataMember]
        public HLinkPersonRefModelCollection GPersonRefCollection { get; set; } = new HLinkPersonRefModelCollection();

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection { get; set; } = new HLinkTagModelCollection();

        /// <summary>
        /// Gets or sets the URL collection.
        /// </summary>
        /// <value>
        /// The URL collection.
        /// </value>
        [DataMember]
        public OCURLModelCollection GURLCollection { get; set; } = new OCURLModelCollection();

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkPersonModel HLink
        {
            get
            {
                HLinkPersonModel t = new HLinkPersonModel
                {
                    HLinkKey = HLinkKey,
                };

                return t;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is living.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is living; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsLiving
        {
            get
            {
                return _IsLiving;
            }

            set
            {
                SetProperty(ref _IsLiving, value);
            }
        }

        /// <summary>
        /// Gets the is living as string.
        /// </summary>
        /// <value>
        /// The is living as string.
        /// </value>
        public string IsLivingAsString
        {
            get
            {
                return _IsLiving.ToString(System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Gets or sets the sibling reference collection.
        /// </summary>
        /// <value>
        /// The sibling reference collection.
        /// </value>
        public HLinkPersonModelCollection SiblingRefCollection
        {
            get
            {
                return _SiblingRefCollection;
            }

            //set
            //{
            //    SetProperty(ref _SiblingRefCollection, value);
            //}
        }

        /// <summary>
        /// Compare two PersonModels.
        /// </summary>
        /// <param name="a">
        /// first object.
        /// </param>
        /// <param name="b">
        /// second object.
        /// </param>
        /// <returns>
        /// returns 1, 2, or 3.
        /// </returns>
        public new int Compare(object a, object b)
        {
            if ((a is null) || (b is null))
            {
                return 0;   // equal
            }

            PersonModel firstPersonModel = (PersonModel)a;
            PersonModel secondPersonModel = (PersonModel)b;

            // compare on surnname first
            int testFlag = string.Compare(firstPersonModel.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname, secondPersonModel.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname, StringComparison.CurrentCulture);

            if (testFlag.Equals(0))
            {
                // equal so check firstname
                testFlag = string.Compare(firstPersonModel.GPersonNamesCollection.GetPrimaryName.DeRef.GFirstName, secondPersonModel.GPersonNamesCollection.GetPrimaryName.DeRef.GFirstName, StringComparison.CurrentCulture);
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
            PersonModel secondPersonModel = (PersonModel)obj;

            // compare on surnname first
            int testFlag = string.Compare(GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname, secondPersonModel.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname, StringComparison.CurrentCulture);

            if (testFlag.Equals(0))
            {
                // equal so check firstname
                testFlag = string.Compare(GPersonNamesCollection.GetPrimaryName.DeRef.GFirstName, secondPersonModel.GPersonNamesCollection.GetPrimaryName.DeRef.GFirstName, StringComparison.CurrentCulture);
            }

            return testFlag;
        }
    }
}