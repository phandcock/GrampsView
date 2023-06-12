// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;

using System.Diagnostics.Contracts;

namespace GrampsView.UserControls
{
    /// <summary>
    /// Code behind for Source Card.
    /// </summary>
    public partial class SourceCardSmall : SmallCardControlTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceCardSmall"/> class.
        /// </summary>
        public SourceCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("SourceCardSmall", args);
        }

        private void Grid_BindingContextChanged(object sender, System.EventArgs e)
        {
            SourceCardSmall card = sender as SourceCardSmall;

            if (card is null)
            {
                this.IsVisible = false;
                return;
            }

            Contract.Requires(BindingContext is HLinkSourceModel);
        }
    }
}