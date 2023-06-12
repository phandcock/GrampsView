// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class DateValCardSingle : SmallCardControlTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateValCardSingle"/> class.
        /// </summary>
        public DateValCardSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("DateValCardSingle", args);
        }
    }
}