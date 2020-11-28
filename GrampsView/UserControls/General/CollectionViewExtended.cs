namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public class CollectionViewExtended : CollectionView
    {
        private int _NumColumns = 10;

        public CollectionViewExtended()
        {
            this.ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems;

            //GridItemsLayout newGridLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical)
            //{
            //    HorizontalItemSpacing = 2,
            //    VerticalItemSpacing = 2,
            //    Span = NumColumns
            //};

            //this.ItemsLayout = newGridLayout;
        }

        /// <summary>
        /// Gets or sets the number columns to display.
        /// </summary>
        /// <value>
        /// The number columns.
        /// </value>
        //public int NumColumns
        //{
        //    get
        //    {
        //        return _NumColumns;
        //    }

        //    set
        //    {
        //        _NumColumns = value;
        //        OnPropertyChanged();
        //    }
        //}
    }
}