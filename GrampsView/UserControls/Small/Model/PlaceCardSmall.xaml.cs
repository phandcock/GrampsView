// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.HLinks.Models;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class PlaceCardSmall : SmallCardControlTemplate
    {
        public PlaceCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new PlaceDetailPage(args.Parameter as HLinkPlaceModel));
        }
    }
}