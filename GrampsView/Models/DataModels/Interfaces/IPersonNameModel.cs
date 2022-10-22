using GrampsView.Models.DataModels.Minor;

using System;
using System.Collections;
using System.ComponentModel;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// </summary>
    public interface IPersonNameModel : IModelBase, IComparable<PersonNameModel>, INotifyPropertyChanged, IComparable, IComparer
    {
        string FirstFirstName { get; }

        DateObjectModel GDate { get; set; }

        string GDisplay { get; set; }
        string GTitle { get; }

        string GType { get; }

        HLinkPersonNameModel HLink { get; }

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
        /// <param name="argObj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        new int CompareTo(object argObj);
    }
}