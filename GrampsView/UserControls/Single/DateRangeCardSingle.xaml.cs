// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class DateRangeCardSingle : SingleCardControlTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRangeCardSingle"/> class.
        /// </summary>
        public DateRangeCardSingle()
        {
            InitializeComponent();
        }
        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("DateRangeCardSingle", args);
        }
    }
}