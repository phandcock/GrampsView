namespace GrampsView.Common
{
    using System.Collections.Generic;

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
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public Group()
        {
        }
    }
}