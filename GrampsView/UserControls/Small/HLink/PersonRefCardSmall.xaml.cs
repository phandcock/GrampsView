namespace GrampsView.UserControls
{
    public partial class PersonRefCardSmall : SmallCardControlTemplate
    {
        public PersonRefCardSmall()
        {
            InitializeComponent();
        }

        void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            //Navigation.PushAsync(new NoteDetailPage(args.Parameter as HLinkNoteModel));
        }
    }
}