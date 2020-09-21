/// <summary>
/// </summary>
namespace GrampsView.Common
{
    using System;
    using System.Collections.Generic;
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
            get; set;
        } = true;

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

                if (!(Items is null) && (Items.Count > 0))
                {
                    Visible = true;
                }
                else
                {
                    Visible = false;
                }
            }
        }

        private void Cards_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // TODO What?
            throw new NotImplementedException();
        }
    }
}