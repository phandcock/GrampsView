namespace GrampsView.UserControls
{
    using GrampsView.Models.HLinks;

    using SharedSharp.Common.Interfaces;

    public partial class MediaCardLarge : Grid
    {
        public MediaCardLarge()
        {
            InitializeComponent();
        }

        public double MediaDetailImageHeight { get; set; } = 100;

        public double MediaDetailImageWidth { get; set; } = 100;


        private void MediaCardLargeRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            MediaCardLarge? thisObject = sender as MediaCardLarge;

            HLinkBase? theHLink = thisObject.BindingContext as HLinkBase;

            if (theHLink != null)
            {
                this.AnchorImage.BindingContext = theHLink.HLinkGlyphItem;
            }

            MediaDetailImageHeight = Ioc.Default.GetService<ISharedSharpCardSizes>().MediaDetailImageHeight;
            MediaDetailImageWidth = Ioc.Default.GetService<ISharedSharpCardSizes>().MediaDetailImageWidth;

        }
    }
}