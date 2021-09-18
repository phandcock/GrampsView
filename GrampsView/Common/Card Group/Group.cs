namespace GrampsView.Common
{
    using System.Collections.Generic;

    using Xamarin.CommunityToolkit.ObjectModel;

    public class Group<T> : ObservableRangeCollection<T>
    {
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