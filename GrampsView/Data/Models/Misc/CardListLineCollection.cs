namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableRangeCollection<CardListLine>))]
    public class CardListLineCollection : ObservableRangeCollection<CardListLine>
    {
        public CardListLineCollection()
        {
        }

        public CardListLineCollection(string argTitle)
        {
            Title = argTitle;
        }

        public string Title { get; set; }

        public bool Visible
        {
            get
            {
                return (Items.Count > 0);
            }
        }

        public new void Add(CardListLine newLine)
        {
            if (newLine is null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(newLine.Value))
            {
                Items.Add(newLine);
            }
        }

        /// <summary>
        /// Replaces the item list with the specified arguments.
        /// </summary>
        /// <param name="theArgs">
        /// The arguments.
        /// </param>
        public void Set(CardListLineCollection theArgs)
        {
            if (theArgs is null)
            {
                return;
            }

            Items.Clear();

            foreach (CardListLine item in theArgs)
            {
                Items.Add(item);
            }
        }
    }
}