namespace GrampsView.UserControls
{
    using GrampsView.Common;

    public partial class FlexMultiCard : FlexLayoutEx

    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlexMultiCard"/> class.
        /// </summary>
        public FlexMultiCard()
        {
            InitializeComponent();
        }

        private void FlexSingleCardRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
        }

        //public static void OnItemsSourceChanged(BindableObject argSource, object oldValue, object newValue)
        //{
        //    Contract.Assert(argSource != null);

        // FlexMultiCard thisCard = argSource as FlexMultiCard;

        // if (newValue is null) { thisCard.IsVisible = false; return; }

        // // Bubble up items changed thisCard.FsctSource.CollectionChanged += FsctSource_CollectionChanged;

        // // TODO cleanup this code when we work out how IEnumerator counter = thisCard.FsctSource.GetEnumerator();

        // if (counter.MoveNext()) { // We have some data thisCard.IsVisible = true; } else {
        // thisCard.IsVisible = false; }

        //    // Set Justification to Center if only one column
        //    if (CardSizes.Current.CardsAcrossColumns == 1)
        //    {
        //        thisCard.theCollectionView.JustifyContent = FlexJustify.Center;
        //    }
        //    else
        //    {
        //        thisCard.theCollectionView.JustifyContent = FlexJustify.Start;
        //    }
        //}
    }
}