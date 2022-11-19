namespace GrampsView.UserControls
{
    using GrampsView.Models.HLinks;

  

    public partial class MediaCardLarge : Grid
    {
        public MediaCardLarge()
        {
            InitializeComponent();
        }

        private void MediaCardLargeRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            MediaCardLarge thisObject = sender as MediaCardLarge;

            HLinkBase theHLink = thisObject.BindingContext as HLinkBase;

            if (theHLink != null)
            {
                this.AnchorImage.BindingContext = theHLink.HLinkGlyphItem;
            }
        }
    }
}