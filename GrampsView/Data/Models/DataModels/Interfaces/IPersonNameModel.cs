//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IPersonNameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// </summary>
    public interface IPersonNameModel : IModelBase
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
        int Compare(object a, object b);

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argObj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        int CompareTo(object argObj);
    }
}