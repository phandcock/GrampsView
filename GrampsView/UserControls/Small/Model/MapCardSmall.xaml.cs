// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.HLinks.Models;

namespace GrampsView.UserControls
{
    public partial class MapCardSmall : SmallCardControlTemplate
    {
        public MapCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            (args.Parameter as HLinkMapModel).DeRef.OpenMap();
        }
    }
}