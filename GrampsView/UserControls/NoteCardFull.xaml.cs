namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using Xamarin.Forms;

    /// <summary>
    /// Code behind for Note Card Large.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl"/>
    public partial class NoteCardFull : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCardFull"/> class.
        /// </summary>
        public NoteCardFull()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the BindingContextChanged event of the NoteCardFullRoot control. Only shows the
        /// note if it is valid.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
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