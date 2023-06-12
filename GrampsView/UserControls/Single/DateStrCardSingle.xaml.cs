// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class DateStrCardSingle : SingleCardControlTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateStrCardSingle"/> class.
        /// </summary>
        public DateStrCardSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("DateStrCardSingle", args);
        }
    }
}