namespace GrampsView.Data.Model
{
    using System.Collections.Generic;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// </summary>

    public class CardListLineCollection : ObservableRangeCollection<CardListLine>
    {
        public CardListLineCollection()
        {
        }

        public CardListLineCollection(string argTitle)
        {
            Title = argTitle;
        }

        public string Title
        {
            get; set;
        }

        public IDictionary<string, string> ToDictionary
        {
            get
            {
                IDictionary<string, string> returnValue = new Dictionary<string, string>();

                foreach (CardListLine item in this)
                {
                    returnValue.Add(item.Label, item.Value);
                }

                return returnValue;
            }
        }

        public bool Visible
        {
            get
            {
                return Items.Count > 0;
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

        public void Add(string argLabel, string argValue)
        {
            if (!string.IsNullOrEmpty(argValue))
            {
                Items.Add(new CardListLine(argLabel, argValue));
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