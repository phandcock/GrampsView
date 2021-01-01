namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using Xamarin.Forms;

    public partial class NoteCardFull : Grid
    {
        public NoteCardFull()
        {
            InitializeComponent();
        }

        private void NoteCardFullRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            NoteCardFull card = (sender as NoteCardFull);

            if ((card is null) || (card.BindingContext is null))
            {
                this.IsVisible = false;
                return;
            }
        }

        private void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            DragGestureRecognizer card = (sender as DragGestureRecognizer);

            INoteModel DaNote = (card.BindingContext as HLinkNoteModel).DeRef;

            e.Data.Text = DaNote.GetDefaultText;
        }
    }
}