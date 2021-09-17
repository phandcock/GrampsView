namespace GrampsView.Common
{
    using GrampsView.Data.Model;

    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    using Xamarin.CommunityToolkit.ObjectModel;

    public class CardGroupModel<T> : ObservableRangeCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
          where T : ModelBase, new()
    {
        public CardGroupModel()
        {
        }

        /// <summary>
        ///// Initializes a new instance of the <see cref="CardGroup"/> class with the ModelBase for a
        ///// single card.
        ///// </summary>
        ///// <param name="argCard">
        ///// The argument card.
        ///// </param>
        //public CardGroupModel(ModelBase argCard)
        //{
        //    CardGroupModel<T> t = new CardGroupModel<T>
        //    {
        //        argCard
        //    };

        //    base.Add(t);
        //}

        public CardGroupModel(IEnumerable<T> argList)
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
        }

        /// <summary>
        /// Gets a value indicating whether [control visible].
        /// </summary>
        /// <value>
        /// <c> true </c> if [control visible]; otherwise, <c> false </c>.
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
            if (argItem.Valid)
            {
                // Check if a duplicate
                if (this.Contains(argItem))
                {
                    return;
                }

                base.Add(argItem);
            }
        }

        public void Add(CardGroupModel argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        public void Add(CardGroupModel<SrcAttributeModel> argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        public void Add(CardGroupModel<T> argCardGroup, string argTitle)
        {
            if (argCardGroup is null)
            {
                throw new ArgumentNullException(nameof(argCardGroup));
            }

            if (argTitle is null)
            {
                throw new ArgumentNullException(nameof(argTitle));
            }

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);

                this.Title = argTitle;
            }
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

        private void Element_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}