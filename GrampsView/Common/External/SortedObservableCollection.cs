//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="SortedObservableCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A Sorted ObservableCollection.
    /// - Sorts on Insert.
    /// - Requires that T implements IComparable.
    /// </summary>
    /// <typeparam name="T">
    /// The type held within collection.
    /// </typeparam>
    public class SortedObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Initialize a simple random number generator.
        /// </summary>
        private readonly Random localRandomNumberGenerator = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedObservableCollection{T}" /> class.
        /// </summary>
        public SortedObservableCollection()
        {
            //// iocGrampsViewCommonProgress = grampsViewCommonProgress;
        }

        /// <summary>
        /// returns a random object from the objects recorded.
        /// </summary>
        /// <returns>
        /// T item.
        /// </returns>
        public T RandomItem()
        {
            if (Items.Count > 0)
            {
                int rndItem = localRandomNumberGenerator.Next(0, Items.Count);

                return Items[rndItem];
            }
            else
            {
                // return nothing
                return default(T);
            }
        }
    }
}