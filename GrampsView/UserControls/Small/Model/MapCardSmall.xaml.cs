// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class MapCardSmall : SmallCardControlTemplate
    {
        public MapCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            //Navigation.PushAsync(new NoteDetailPage(args.Parameter as HLinkNoteModel));
        }
    }
}