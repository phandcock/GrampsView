namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using Xamarin.Forms;

    public partial class MediaCardLarge : Grid
    {
        public MediaCardLarge()
        {
            InitializeComponent();
        }

        private void MediaCardLargeRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            MediaCardLarge thisObject = sender as MediaCardLarge;

            if (thisObject.BindingContext is null)
            {
                (sender as MediaCardLarge).BindingContext = new HLinkMediaModel();
            }
        }
    }
}