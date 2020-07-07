//-----------------------------------------------------------------------
//
// Colour Picker brush routines
//
// <copyright file="CommonGroupInfoCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Holds a Group List suitable for Semantic Zoom.
    /// </summary>
    public class CommonGroupInfoCollection<T> : List<T>
    {
        /// <summary>
        /// Gets or sets the key to the Group List.
        /// </summary>
        public object Key
        {
            get;
            set;
        }

        /// <summary>
        /// Returns an enumerator.
        /// </summary>
        /// <returns>
        /// An enumerator.
        /// </returns>
        public new IEnumerator<T> GetEnumerator()
        {
            return (System.Collections.Generic.IEnumerator<T>)base.GetEnumerator();
        }
    }
}