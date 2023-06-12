// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class TagCardSingle : SingleCardControlTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagCardSingle"/> class.
        /// </summary>
        public TagCardSingle()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("TagCardSingle", args);
        }
    }
}