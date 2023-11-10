// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels.Date;
using GrampsView.ModelsDB.Collections.HLinks;

namespace GrampsView.Models.DataModels.Minor
{
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
    /// <seealso cref="ModelBase"/>
    /// <seealso cref="IPersonNameModel"/>
    /// <seealso cref="IComparable"/>
    /// <seealso cref="System.Collections.IComparer"/>

    public class PersonNameModel : ModelBase, IPersonNameModel
    {
        public PersonNameModel()
        {
            ModelItemGlyph.Symbol = Constants.IconPersonName;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPerson");
        }

        public override string DefaultTextShort
        {
            get
            {
                string fullName = FirstFirstName + " " + GSurName.GetPrimarySurname;
                return fullName.Trim().Length == 0 ? "Unknown" : fullName;
            }
        }

        public string ExtraNames => !string.IsNullOrEmpty(GCall)
                    ? $"Called: {GCall}"
                    : !string.IsNullOrEmpty(GNick)
                    ? $"Nickname: {GNick}"
                    : !string.IsNullOrEmpty(GFamilyNick) ? $"Family Nickname: {GFamilyNick}" : string.Empty;

        public string FirstFirstName => GFirstName.Split()[0];

        /// <summary>
        /// Gets the Persons FullName. Returns 'unknown' if no firstname or surname.
        /// </summary>
        public string FullName
        {
            get
            {
                string fullName = GFirstName + " " + GSurName.GetPrimarySurname;
                return fullName.Trim().Length == 0 ? "Unknown" : fullName;
            }
        }

        /// <summary>
        /// Gets or sets the alt. details.
        /// </summary>
        /// <value>
        /// The g alt.
        /// </value>

        public AltModel GAlt { get; set; } = new AltModel();

        /// <summary>
        /// Gets or sets the call details.
        /// </summary>
        /// <value>
        /// The g call.
        /// </value>

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

        public HLinkCitationDBModelCollection GCitationRefCollection
        {
            get; set;
        }

        = new HLinkCitationDBModelCollection();

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>

        public DateObjectModelBase GDate { get; set; } = new DateObjectModelVal();

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The g display.
        /// </value>

        public string GDisplay
        {
            get;
            set;
        }

          = string.Empty;

        /// <summary>
        /// Gets or sets the family nick.
        /// </summary>
        /// <value>
        /// The g family nick.
        /// </value>

        public string GFamilyNick
        {
            get; set;
        }

          = string.Empty;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name of the g.
        /// </value>

        public string GFirstName
        {
            get;

            set;
        }
          = string.Empty;

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The g group.
        /// </value>

        public string GGroup
        {
            get; set;
        }

          = string.Empty;

        /// <summary>
        /// Gets or sets the nick.
        /// </summary>
        /// <value>
        /// The g nick.
        /// </value>

        public string GNick
        {
            get; set;
        }

          = string.Empty;

        /// <summary>
        /// Gets or sets the note reference collection.
        /// </summary>
        /// <value>
        /// The note reference collection.
        /// </value>

        public HLinkNoteDBModelCollection GNoteReferenceCollection
        {
            get; set;
        }

        = new HLinkNoteDBModelCollection();

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The g sort.
        /// </value>

        public string GSort
        {
            get;

            set;
        }

          = string.Empty;


        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>
        /// The g suffix.
        /// </value>

        public string GSuffix
        {
            get; set;
        }

          = string.Empty;

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The name of the g sur.
        /// </value>

        public HLinkSurnameModelCollection GSurName
        {
            get;

            set;
        }

        = new HLinkSurnameModelCollection();

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The g title.
        /// </value>

        public string GTitle
        {
            get; set;
        }

          = string.Empty;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type of the g.
        /// </value>

        public string GType
        {
            get;
            set;
        }

          = string.Empty;

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
                return SharedConstants.CompareEquals;
            }

            if (b is null)
            {
                return SharedConstants.CompareEquals;
            }

            PersonNameModel firstPersonName = (PersonNameModel)a;
            PersonNameModel secondPersonName = (PersonNameModel)b;

            // Compare on Surname first
            int testFlag = string.Compare(firstPersonName.GSurName.GetPrimarySurname, secondPersonName.GSurName.GetPrimarySurname, StringComparison.CurrentCulture);

            if (testFlag == SharedConstants.CompareEquals)
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
                return SharedConstants.CompareGreaterThan;
            }
            // Compare on Surname first
            int testFlag = string.Compare(GSurName.GetPrimarySurname, other.GSurName.GetPrimarySurname, StringComparison.CurrentCulture);

            if (testFlag == SharedConstants.CompareEquals)
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
            return obj is null
                ? SharedConstants.CompareEquals
                : !(obj is PersonNameModel secondPersonName) ? SharedConstants.CompareEquals : CompareTo(secondPersonName);
        }

        /// <summary>
        /// Gets the default text for this ViewModel.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public override string ToString()
        {
            return FullName;
        }
    }
}