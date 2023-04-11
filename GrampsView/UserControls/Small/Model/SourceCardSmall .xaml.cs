// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

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

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new SourceDetailPage(args.Parameter as HLinkSourceModel));
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