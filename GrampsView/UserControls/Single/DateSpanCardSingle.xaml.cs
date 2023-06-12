// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class DateSpanCardSingle : SingleCardControlTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateSpanCardSingle"/> class.
        /// </summary>
        public DateSpanCardSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("DateSpanCardSingle", args);
        }
    }
}