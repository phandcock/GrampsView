// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// TODO Update fields as per Schema Class for holding a person's name.
    /// <list type="table">
    /// <item>
    /// <description> Attribute </description>
    /// <description> Actioned </description>
    /// </item>
    /// <item>
    /// <description> Gramps XML 1.71 attribute </description>
    /// <description> Yes </description>
    /// </item>
    /// </list>
    /// </summary>
    /// <seealso cref="GrampsView.Data.Model.ModelBase"/>
    /// <seealso cref="GrampsView.Data.Model.IPersonNameModel"/>
    /// <seealso cref="System.IComparable"/>
    /// <seealso cref="System.Collections.IComparer"/>
    [DataContract]
    public class PersonNameModel : ModelBase, IPersonNameModel
    {
        public PersonNameModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconPersonName;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        /// <summary>
        /// Gets the default text for this ViewModel.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public override string DefaultText
        {
            get
            {
                return FullName;
            }
        }

        public override string DefaultTextShort
        {
            get
            {
                string fullName = FirstFirstName + " " + GSurName.GetPrimarySurname;
                if (fullName.Trim().Length == 0)
                {
                    return "Unknown";
                }
                else
                {
                    return fullName;
                }
            }
        }

        public string ExtraNames
        {
            get
            {
                if (!string.IsNullOrEmpty(GCall))
                {
                    return $"Called: {GCall}";
                }

                if (!string.IsNullOrEmpty(GNick))
                {
                    return $"Nickname: {GNick}";
                }

                if (!string.IsNullOrEmpty(GFamilyNick))
                {
                    return $"Family Nickname: {GFamilyNick}";
                }

                return string.Empty;
            }
        }

        public string FirstFirstName
        {
            get
            {
                return GFirstName == null ? string.Empty : GFirstName.Split()[0];
            }
        }

        /// <summary>
        /// Gets the Persons FullName. Returns 'unknown' if no firstname or surname.
        /// </summary>
        public string FullName
        {
            get
            {
                string fullName = GFirstName + " " + GSurName.GetPrimarySurname;
                if (fullName.Trim().Length == 0)
                {
                    return "Unknown";
                }
                else
                {
                    return fullName;
                }
            }
        }

        /// <summary>
        /// Gets or sets the alt. details.
        /// </summary>
        /// <value>
        /// The g alt.
        /// </value>
        [DataMember]
        public IAltModel GAlt { get; set; } = new AltModel();

        /// <summary>
        /// Gets or sets the call details.
        /// </summary>
        /// <value>
        /// The g call.
        /// </value>
        [DataMember]
        public string GCall
        {
            get; set;
        }

        = string.Empty;

        /// <summary>
        /// Gets or sets the citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get; set;
        }

        = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [DataMember]
        public DateObjectModel GDate { get; set; } = new DateObjectModelVal();

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The g display.
        /// </value>
        [DataMember]
        public string GDisplay
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the family nick.
        /// </summary>
        /// <value>
        /// The g family nick.
        /// </value>
        [DataMember]
        public string GFamilyNick
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name of the g.
        /// </value>
        [DataMember]
        public string GFirstName
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The g group.
        /// </value>
        [DataMember]
        public string GGroup
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the nick.
        /// </summary>
        /// <value>
        /// The g nick.
        /// </value>
        [DataMember]
        public string GNick
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the note reference collection.
        /// </summary>
        /// <value>
        /// The note reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteReferenceCollection
        {
            get; set;
        }

        = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The g sort.
        /// </value>
        [DataMember]
        public string GSort
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>
        /// The g suffix.
        /// </value>
        [DataMember]
        public string GSuffix
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The name of the g sur.
        /// </value>
        [DataMember]
        public SurnameModelCollection GSurName
        {
            get;

            set;
        }

        = new SurnameModelCollection();

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The g title.
        /// </value>
        [DataMember]
        public string GTitle
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type of the g.
        /// </value>
        [DataMember]
        public string GType
        {
            get;
            set;
        }

        public HLinkPersonNameModel HLink
        {
            get
            {
                HLinkPersonNameModel t = new HLinkPersonNameModel
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
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
                return CommonConstants.CompareEquals;
            }

            if (b is null)
            {
                return CommonConstants.CompareEquals;
            }

            PersonNameModel firstPersonName = (PersonNameModel)a;
            PersonNameModel secondPersonName = (PersonNameModel)b;

            // Compare on Surname first
            int testFlag = string.Compare(firstPersonName.GSurName.GetPrimarySurname, secondPersonName.GSurName.GetPrimarySurname, StringComparison.CurrentCulture);

            if (testFlag == CommonConstants.CompareEquals)
            {
                // Compare on first name
                testFlag = string.Compare(firstPersonName.GFirstName, secondPersonName.GFirstName, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        public int CompareTo(PersonNameModel other)
        {
            if (other is null)
            {
                return CommonConstants.CompareGreaterThan;
            }
            // Compare on Surname first
            int testFlag = string.Compare(GSurName.GetPrimarySurname, other.GSurName.GetPrimarySurname, StringComparison.CurrentCulture);

            if (testFlag == CommonConstants.CompareEquals)
            {
                // Compare on first name
                testFlag = string.Compare(GFirstName, other.GFirstName, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// The object to compare.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        public override int CompareTo(object obj)
        {
            if (obj is null)
            {
                return CommonConstants.CompareEquals;
            }

            PersonNameModel secondPersonName = obj as PersonNameModel;

            if (secondPersonName is null)
            {
                return CommonConstants.CompareEquals;
            }

            return CompareTo(secondPersonName);
        }
    }
}