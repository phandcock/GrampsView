// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.HLinks.Models;
using GrampsView.Views;

namespace GrampsView.UserControls
{
    public partial class EventCardSmall : SmallCardControlTemplate
    {
        public EventCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            Navigation.PushAsync(new EventDetailPage(args.Parameter as HLinkEventModel));
        }
    }
}