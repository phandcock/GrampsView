namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    using static GrampsView.Common.CommonEnums;

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

    [KnownType(typeof(HLinkFamilyModel))]
    public sealed class PersonModel : ModelBase, IPersonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        public PersonModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconPeople;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [JsonInclude]
        public DateObjectModel BirthDate
        {
            get; set;
        }

            = new DateObjectModelVal();

        /// <summary>
        /// Gets or sets gaddress collection.
        /// </summary>
        [JsonInclude]
        public HLinkAddressModelCollection GAddressCollection
        {
            get; set;
        }

        = new HLinkAddressModelCollection();

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
        public HLinkFamilyModel GChildOf
        {
            get;

            set;
        } = new HLinkFamilyModel();

        /// <summary>
        /// Gets or sets the citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>
        [JsonInclude]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get;

            set;
        } = new HLinkCitationModelCollection();

        /// <summary> Gets or sets the Event Reference Collection.
        // </summary>
        [JsonInclude]
        public HLinkEventModelCollection GEventRefCollection
        {
            get;

            set;
        } = new HLinkEventModelCollection();

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [JsonInclude]
        public Gender GGender
        {
            get;

            set;
        } = Gender.Unknown;

        /// <summary>
        /// Gets or sets the LDS collection.
        /// </summary>
        /// <value>
        /// The GLDS collection.
        /// </value>
        [JsonInclude]
        public OCLdsOrdModelCollection GLDSCollection
        {
            get;

            set;
        } = new OCLdsOrdModelCollection();

        /// <summary> Gets or sets Media In $$(hLink)$$.
        // </summary>
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
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get;

            set;
        } = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the parent relationship collection.
        /// </summary>
        [JsonInclude]
        public HLinkFamilyModelCollection GParentInRefCollection
        {
            get;

            set;
        } = new HLinkFamilyModelCollection();

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
                HLinkPersonModel t = new HLinkPersonModel
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
        public string IsLivingAsString
        {
            get
            {
                return IsLiving.ToString(System.Globalization.CultureInfo.CurrentCulture);
            }
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

        public override string ToString() => GPersonNamesCollection.GetPrimaryName.DeRef.ToString();
    }
}