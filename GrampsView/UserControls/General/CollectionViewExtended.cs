﻿namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using System;
    using System.ComponentModel;

    using Xamarin.Forms;

    public class CollectionViewExtended : CollectionView
    {
        private readonly GridItemsLayout newGridLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical)
        {
            HorizontalItemSpacing = 2,
            VerticalItemSpacing = 2,
            Span = 10,
        };

        private int _NumColumns = 10;

        private int _NumItems = 10;

        public CollectionViewExtended()
        {
            this.ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems;

            this.ItemsLayout = newGridLayout;
        }

        /// <summary>
        /// Occurs when we want to trigger a property changed event.
        /// </summary>
        public event PropertyChangedEventHandler MyPropertyChanged;

        /// <summary>
        /// Gets or sets the number columns to display.
        /// </summary>
        /// <value>
        /// The number columns.
        /// </value>
        public int NumColumns
        {
            get
            {
                return _NumColumns;
            }

            set
            {
                if ((value > 0) && (value < 20))
                {
                    _NumColumns = value;
                    OnPropertyChanged();
                }
                else
                {
                }
            }
        }

        public int NumItems
        {
            get
            {
                return _NumItems;
            }

            set
            {
                _NumItems = value;
                OnPropertyChanged();
            }
        }

        public void SetUcHeight()
        {
            int t = (Convert.ToInt32(NumItems / NumColumns) + 1);
            int ucHeight = Convert.ToInt32(t * CardSizes.Current.CardSmallHeight);  // +1 for padding
            ucHeight = ucHeight + 50;
            this.HeightRequest = ucHeight;
        }

        /// <summary>
        /// Method that is called when a bound property is changed.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the bound property that changed.
        /// </param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected new void OnPropertyChanged(string propertyName)
        {
            MyPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            int t = (Int32)(width / CardSizes.Current.CardSmallWidth);

            NumColumns = t;

            newGridLayout.Span = t;

            this.ItemsLayout = newGridLayout;

            SetUcHeight();
        }
    }
}