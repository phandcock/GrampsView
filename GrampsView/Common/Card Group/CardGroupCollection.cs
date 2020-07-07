//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="CardGroupCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>
    [CollectionDataContract]
    public class CardGroupCollection : ObservableCollection<CardGroup>
    {
        public CardGroupCollection()
        {
            this.CollectionChanged += FullObservableCollectionCollectionChanged;
        }

        /// <summary>
        /// Adds the specified argument card group.
        /// </summary>
        /// <param name="argCardGroup">
        /// The argument card group.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// argCardGroup
        /// </exception>
        public new void Add(CardGroup argCardGroup)
        {
            if (argCardGroup is null)
            {
                throw new ArgumentNullException(nameof(argCardGroup));
            }

            if (argCardGroup.Visible)
            {
                base.Add(argCardGroup);
            }
        }

        /// <summary>
        /// Adds the specified argument card group but using the supplied title.
        /// </summary>
        /// <param name="argCardGroup">
        /// The argument card group.
        /// </param>
        /// <param name="argTitle">
        /// The argument title.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// argCardGroup
        /// </exception>
        public void Add(CardGroup argCardGroup, string argTitle)
        {
            if (argCardGroup is null)
            {
                throw new ArgumentNullException(nameof(argCardGroup));
            }

            argCardGroup.Title = argTitle;
            Add(argCardGroup);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void FullObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("CollectionChanged");
        }
    }
}