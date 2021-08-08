using System;
using System.Collections;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// </summary>
    public interface IPeopleGraphNode : IModelBase, IComparable<PeopleGraphNode>, IComparable, IComparer
    {
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