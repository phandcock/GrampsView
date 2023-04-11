// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class SourceCardLinkCell : LinkCellCardControlTemplate
    {
        public SourceCardLinkCell()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new SourceDetailPage(args.Parameter as HLinkSourceModel));
        }
    }
}