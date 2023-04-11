namespace GrampsView.UserControls
{
    public partial class NameMapCardSmall : SmallCardControlTemplate
    {
        public NameMapCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            //Navigation.PushAsync(new NoteDetailPage(args.Parameter as HLinkNoteModel));
        }
    }
}