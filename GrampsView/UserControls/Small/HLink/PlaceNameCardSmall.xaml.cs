// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class PlaceNameCardSmall : SmallCardControlTemplateNS
    {
        public PlaceNameCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            //      Navigation.PushAsync(new placename(args.Parameter as HLinkNoteModel));
        }
    }
}