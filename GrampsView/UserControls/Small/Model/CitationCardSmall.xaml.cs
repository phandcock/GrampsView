// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class CitationCardSmall : SmallCardControlTemplate
    {
        public CitationCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new CitationDetailPage(args.Parameter as HLinkCitationModel));
        }
    }
}