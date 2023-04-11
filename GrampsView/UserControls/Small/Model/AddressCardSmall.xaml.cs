// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class AddressCardSmall : SmallCardControlTemplate
    {
        public AddressCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            //Navigation.PushAsync(new NoteDetailPage(args.Parameter as HLinkNoteModel));
        }
    }
}