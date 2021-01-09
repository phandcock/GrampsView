/// <summary>
/// </summary>
namespace GrampsView.Common
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    using Xamarin.CommunityToolkit.ObjectModel;

    public delegate void ListedItemPropertyChangedEventHandler(IList SourceList, object Item, PropertyChangedEventArgs e);

    /// <summary>
    /// </summary>
    public class CardGroupBase<T> : ObservableRangeCollection<T>, INotifyCollectionChanged
    {
        private ListedItemPropertyChangedEventHandler ItemPropertyChanged;

        public CardGroupBase()
        {
            this.CollectionChanged += Cards_CollectionChanged;
        }

        public CardGroupBase(IEnumerable<T> argList)
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
                return !(Items is null) && (Items.Count > 0);
            }
        }

        public new void Add(T argItem)
        {
            // Check if a duplicate
            if (this.Contains(argItem))
            {
                return;
            }

            base.Add(argItem);
        }

        public new void Clear()
        {
            foreach (var item in this)
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
                foreach (var item in e.OldItems)
                {
                    if (item != null && item is INotifyPropertyChanged i)
                    {
                        i.PropertyChanged -= Element_PropertyChanged;
                    }
                }
            }

            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item != null && item is INotifyPropertyChanged i)
                    {
                        i.PropertyChanged -= Element_PropertyChanged;
                        i.PropertyChanged += Element_PropertyChanged;
                    }
                }
            }
        }

        private void Element_PropertyChanged(object sender, PropertyChangedEventArgs e) => ItemPropertyChanged?.Invoke(this, sender, e);
    }
}