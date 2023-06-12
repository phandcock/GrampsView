// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.UserControls
{
    public partial class NoteCardSmall : SmallCardControlTemplate
    {
        public NoteCardSmall()
        {
            InitializeComponent();
        }

        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            OnTapGestureRecognizerTappedHandler("NoteCardSmall", args);
        }
    }
}