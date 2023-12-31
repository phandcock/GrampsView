﻿// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.HLinks;

using SharedSharp.Common.CustomClasses;

using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace GrampsView.Common
{
    public delegate void DBListedItemPropertyChangedEventHandler(IList SourceList, object Item, PropertyChangedEventArgs e);

    public class DBCardGroupHLink<T> : SharedSharpObservableRangeCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
          where T : HLinkDBBase, new()
    {
        public DBCardGroupHLink()
        {
            CollectionChanged += Cards_CollectionChanged;
        }

        public DBCardGroupHLink(string argTitle)
        {
            Title = argTitle;

            CollectionChanged += Cards_CollectionChanged;
        }

        public DBCardGroupHLink(IEnumerable<T> argList)
        {
            Contract.Assert(argList != null);

            foreach (T item in argList)
            {
                base.Add(item);
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get; set;
        } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether [control visible].
        /// </summary>
        /// <value>
        /// <c> true </c> if [control visible]; otherwise, <c> false </c>.
        /// </value>
        public bool Visible => Items is not null && (Items.Count > 0);

        public new void Add(T argItem)
        {
            if (argItem.Valid)
            {
                // Check if a duplicate
                if (Contains(argItem))
                {
                    return;
                }

                base.Add(argItem);
            }
        }

        public new void Clear()
        {
            foreach (T item in this)
            {
                if (item is INotifyPropertyChanged i)
                {
                    i.PropertyChanged -= Element_PropertyChanged;
                }
            }

            base.Clear();
        }

        private void Cards_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (T item in e.OldItems)
                {
                    if (item is not null and INotifyPropertyChanged i)
                    {
                        i.PropertyChanged -= Element_PropertyChanged;
                    }
                }
            }

            if (e.NewItems != null)
            {
                foreach (T item in e.NewItems)
                {
                    if (item is not null and INotifyPropertyChanged i)
                    {
                        i.PropertyChanged -= Element_PropertyChanged;
                        i.PropertyChanged += Element_PropertyChanged;
                    }
                }
            }
        }

        private void Element_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}