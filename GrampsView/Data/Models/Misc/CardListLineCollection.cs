// <copyright file="CardListLineCollection.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// Common routines
/// </summary>
namespace GrampsView.Data.Model
{
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<CardListLine>))]
    public class CardListLineCollection : ObservableCollection<CardListLine>
    {
        public CardListLineCollection()
        {
        }

        public string Title { get; set; }

        public bool Visible
        {
            get
            {
                return (Items.Count > 0);
            }
        }

        public new void Add(CardListLine newLine)
        {
            if (newLine is null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(newLine.Value))
            {
                Items.Add(newLine);
            }
        }

        /// <summary>
        /// Replaces the item list with the specified arguments.
        /// </summary>
        /// <param name="theArgs">
        /// The arguments.
        /// </param>
        public void Set(CardListLineCollection theArgs)
        {
            if (theArgs is null)
            {
                return;
            }

            Items.Clear();

            foreach (CardListLine item in theArgs)
            {
                Items.Add(item);
            }
        }
    }
}