namespace GrampsView.Common
{
    using System.Collections.Generic;

    public class Grouping<K, T> : SharedSharpObservableRangeCollection<T>
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