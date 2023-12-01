// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels.Date;
using GrampsView.ModelsDB.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.Models.DataModels
{
    /// <summary>
    /// Data model for a person.
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
    /// </summary>

    [KnownType(typeof(HLinkFamilyDBModel))]
    public sealed class PersonModel : ModelBase, IPersonModel
    {
        private Gender _GGender = Gender.Unknown;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        public PersonModel()
        {
            ModelItemGlyph.Symbol = Constants.IconPeople;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [JsonInclude]
        public DateObjectModelBase BirthDate
        {
            get; set;
        }

            = new DateObjectModelVal();

        /// <summary>
        /// Gets or sets gaddress collection.
        /// </summary>
        [JsonInclude]
        public HLinkAddressDBModelCollection GAddressCollection
        {
            get; set;
        }

        = new HLinkAddressDBModelCollection();

        /// <summary>
        /// Gets or sets the gattribute collection.
        /// </summary>
        /// <value>
        /// The gattribute collection.
        /// </value>
        [JsonInclude]
        public HLinkAttributeModelCollection GAttributeCollection
        {
            get;

            set;
        } = new HLinkAttributeModelCollection();

        /// <summary>
        /// Gets or sets the child of.
        /// </summary>
        /// <value>
        /// The child of.
        /// </value>
        [JsonInclude]
        public HLinkFamilyDBModel GChildOf
        {
            get;

            set;
        } = new HLinkFamilyDBModel();

        /// <summary>
        /// Gets or sets the citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>
        [JsonInclude]
        public HLinkCitationDBModelCollection GCitationRefCollection
        {
            get;

            set;
        } = new HLinkCitationDBModelCollection();

        [JsonInclude]
        public HLinkEventDBModelCollection GEventRefCollection
        {
            get;

            set;
        } = new HLinkEventDBModelCollection();

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [JsonInclude]
        public Gender GGender
        {
            get => _GGender;

            set
            {
                _ = SetProperty(ref _GGender, value);

                switch (value)
                {
                    case Gender.Female:
                        {
                            ModelItemGlyph.Symbol = Constants.IconPersonFemale;
                            break;
                        }

                    case Gender.Male:
                        {
                            ModelItemGlyph.Symbol = Constants.IconPersonMale;
                            break;
                        }
                    case Gender.Unknown:
                    default:
                        {
                            ModelItemGlyph.Symbol = Constants.IconPeople;
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Gets or sets the LDS collection.
        /// </summary>
        /// <value>
        /// The GLDS collection.
        /// </value>
        [JsonInclude]
        public HLinkLdsOrdModelCollection GLDSCollection
        {
            get;

            set;
        } = new HLinkLdsOrdModelCollection();

        [JsonInclude]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get;

            set;
        } = new HLinkMediaModelCollection();

        /// <summary>
        /// Gets or sets the Note reference collection.
        /// </summary>
        /// <value>
        /// The Note reference.
        /// </value>
        [JsonInclude]
        public HLinkNoteDBModelCollection GNoteRefCollection
        {
            get;

            set;
        } = new HLinkNoteDBModelCollection();

        /// <summary>
        /// Gets or sets the parent relationship collection.
        /// </summary>
        [JsonInclude]
        public HLinkFamilyDBModelCollection GParentInRefCollection
        {
            get;

            set;
        } = new HLinkFamilyDBModelCollection();

        /// <summary>
        /// Gets or sets the Persons Birthname.
        /// </summary>
        [JsonInclude]
        public HLinkPersonNameModelCollection GPersonNamesCollection
        {
            get;

            set;
        } = new HLinkPersonNameModelCollection();

        /// <summary>
        /// Gets or sets the person reference collection.
        /// </summary>
        /// <value>
        /// The g person reference collection.
        /// </value>
        [JsonInclude]
        public HLinkPersonRefModelCollection GPersonRefCollection { get; set; } = new HLinkPersonRefModelCollection();

        /// <summary>
        /// Gets or sets the tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [JsonInclude]
        public HLinkTagModelCollection GTagRefCollection { get; set; } = new HLinkTagModelCollection();

        /// <summary>
        /// Gets or sets the URL collection.
        /// </summary>
        /// <value>
        /// The URL collection.
        /// </value>
        [JsonInclude]
        public HLinkURLModelCollection GURLCollection { get; set; } = new HLinkURLModelCollection();

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
                HLinkPersonModel t = new()
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is living.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is living; otherwise, <c> false </c>.
        /// </value>
        [JsonInclude]
        public bool IsLiving
        {
            get;

            set;
        } = false;

        /// <summary>
        /// Gets the is living as string.
        /// </summary>
        /// <value>
        /// The is living as string.
        /// </value>
        public string IsLivingAsString => IsLiving.ToString(System.Globalization.CultureInfo.CurrentCulture);

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
            if (a is null || b is null)
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
        public override int CompareTo(object obj)
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

        public int CompareTo(PersonModel argOther)
        {
            // compare on surnname first
            int testFlag = string.Compare(GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname, argOther.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname, StringComparison.CurrentCulture);

            if (testFlag.Equals(0))
            {
                // equal so check firstname
                testFlag = string.Compare(GPersonNamesCollection.GetPrimaryName.DeRef.GFirstName, argOther.GPersonNamesCollection.GetPrimaryName.DeRef.GFirstName, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        public override string ToString()
        {
            return GPersonNamesCollection.GetPrimaryName.DeRef.ToString();
        }
    }
}