namespace GrampsView.UserControls
{
  

    public partial class NoteCardFull : Grid
    {
        public NoteCardFull()
        {
            InitializeComponent();
        }

        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            Label t = (sender as DragGestureRecognizer).Parent as Label;

            e.Data.Text = (string)t.FormattedText;
        }
    }
}