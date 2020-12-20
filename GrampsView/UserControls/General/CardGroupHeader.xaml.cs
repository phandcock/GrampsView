namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class CardGroupHeader : Frame
    {
        public CardGroupHeader()
        {
            InitializeComponent();
        }

        private void CardGroupHeaderRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            var t = sender;
        }
    }
}