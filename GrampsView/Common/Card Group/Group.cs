// Copyright (c) phandcock.  All rights reserved.

using SharedSharp.Common.CustomClasses;

using System.Collections.Generic;

namespace GrampsView.Common
{
    public class Group<T> : SharedSharpObservableRangeCollection<T>
    {
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

        public Group(List<T> items)
        {
            foreach (T? item in items)
            {
                Items.Add(item);
            }
        }

        public Group()
        {
        }
    }
}