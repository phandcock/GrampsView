// Copyright (c) phandcock.  All rights reserved.

using SharedSharp.Common.CustomClasses;

using System.Collections.Generic;

namespace GrampsView.Common
{
    public class Grouping<K, T> : SharedSharpObservableRangeCollection<T>
    {
        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (T? item in items)
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