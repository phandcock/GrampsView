namespace GrampsView.Common
{
    using System.Collections.Generic;

    using Xamarin.CommunityToolkit.ObjectModel;

    public class Grouping<K, T> : ObservableRangeCollection<T>
    {
        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public K Key
        {
            get; private set;
        }
    }
}