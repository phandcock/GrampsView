// <copyright file="ObservableCollectionExtensions.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// Extensions tot he Observable Collection class
/// </summary>
namespace GrampsView.Common
{
    using GrampsView.Data.Model;

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// </summary>
    public static class ObservableCollection
    {
        public static void AddRange<T>(this ObservableCollection<T> thisObservableCollection, IList<T> newList)
        {
            if (newList is null)
            {
                throw new ArgumentNullException(nameof(newList));
            }

            if (thisObservableCollection is null)
            {
                throw new ArgumentNullException(nameof(thisObservableCollection));
            }

            foreach (var item in newList)
            {
                thisObservableCollection.Add(item);
            }
        }

        /// <summary>
        /// Sorts the specified key selector. See https://stackoverflow.com/questions/1945461/how-do-i-sort-an-observable-collection
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the source.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key.
        /// </typeparam>
        /// <param name="theSource">
        /// The source.
        /// </param>
        /// <param name="keySelector">
        /// The key selector.
        /// </param>
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> theSource, Func<TSource, TKey> keySelector)
        {
            if (theSource is null)
            {
                throw new ArgumentNullException(nameof(theSource));
            }

            List<TSource> sortedList = theSource.OrderBy(keySelector).ToList();
            theSource.Clear();
            foreach (var sortedItem in sortedList)
            {
                theSource.Add(sortedItem);
            }
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> argEnumerable)
        {
            if (argEnumerable is null)
            {
                throw new ArgumentNullException(nameof(argEnumerable));
            }

            var col = new ObservableCollection<T>();
            foreach (var cur in argEnumerable)
            {
                col.Add(cur);
            }

            return col;
        }
    }
}