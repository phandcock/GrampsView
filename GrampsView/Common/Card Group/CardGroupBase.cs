//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="CardGroupCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// </summary>
namespace GrampsView.Common
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// </summary>
    public class CardGroupBase<T> : ObservableCollection<T>, INotifyCollectionChanged
    {
        public CardGroupBase()
        {
            this.CollectionChanged += Cards_CollectionChanged;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets a value indicating whether [control visible].
        /// </summary>
        /// <value>
        /// <c>true</c> if [control visible]; otherwise, <c>false</c>.
        /// </value>
        public bool Visible
        {
            get
            {
                if (!(Items is null) && (Items.Count > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void Cards_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    Contract.Assert(item != null);

                    item.PropertyChanged -= Cards_PropertyChanged;
                }

                if (e.NewItems != null)
                {
                    foreach (INotifyPropertyChanged item in e.NewItems)
                    {
                        Contract.Assert(item != null);

                        item.PropertyChanged += Cards_PropertyChanged;
                    }
                }
            }
        }

        private void Cards_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}