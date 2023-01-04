namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;
    using GrampsView.Models.Collections.HLinks;
    using GrampsView.Models.DataModels;
    using GrampsView.Models.DataModels.Date;

    using System;
    using System.Collections;
    using System.ComponentModel;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// </summary>
    public interface IPersonModel : IModelBase, IComparable<PersonModel>, INotifyPropertyChanged, IComparable, IComparer
    {
        /// <summary>
        /// Gets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        DateObjectModelBase BirthDate
        {
            get;
        }

        /// <summary>
        /// Gets the g attribute collection.
        /// </summary>
        /// <value>
        /// The g attribute collection.
        /// </value>
        HLinkAttributeModelCollection GAttributeCollection
        {
            get;
        }

        /// <summary>
        /// Gets or sets the child of.
        /// </summary>
        /// <value>
        /// The child of.
        /// </value>
        HLinkFamilyModel GChildOf
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the event reference.
        /// </summary>
        /// <value>
        /// The event reference.
        /// </value>
        HLinkEventModelCollection GEventRefCollection
        {
            get;
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        Gender GGender
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the media reference collection.
        /// </summary>
        /// <value>
        /// The media reference collection.
        /// </value>
        HLinkMediaModelCollection GMediaRefCollection
        {
            get;
        }

        /// <summary>
        /// Gets the Note reference collection.
        /// </summary>
        /// <value>
        /// The Note reference.
        /// </value>
        HLinkNoteModelCollection GNoteRefCollection
        {
            get;
        }

        /// <summary>
        /// Gets or sets the parent relationship collection.
        /// </summary>
        /// <value>
        /// The parent relationship collection.
        /// </value>
        HLinkFamilyModelCollection GParentInRefCollection
        {
            get;
        }

        /// <summary>
        /// Gets or sets the Persons Birth Name.
        /// </summary>
        /// <value>
        /// The name of the birth.
        /// </value>
        HLinkPersonNameModelCollection GPersonNamesCollection
        {
            get;
        }

        /// <summary>
        /// Gets the get $$(HLinkPersonModel)$$ that points to this ViewModel.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        HLinkPersonModel HLink
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is living.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is living; otherwise, <c> false </c>.
        /// </value>
        bool IsLiving
        {
            get;
        }

        /// <summary>
        /// Compares the specified a.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>
        new int Compare(object a, object b);

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argObject">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        new int CompareTo(object argObject);
    }
}