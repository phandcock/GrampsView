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
    public class CardGroupCollectionTyped<T> : ObservableCollection<CardGroupBase<T>>
    {
        public CardGroupCollectionTyped()
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
        public new void Add(CardGroupBase<T> argCardGroup)
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
        public void Add(CardGroupBase<T> argCardGroup, string argTitle)
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