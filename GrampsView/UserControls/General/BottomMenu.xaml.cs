namespace GrampsView.UserControls
{
    using GrampsView.Common;

    using Xamarin.Forms;

    public partial class BottomMenu : Frame
    {
        public BottomMenu()
        {
            InitializeComponent();
        }

        public string HubButtonGlyph
        {
            get
            {
                return CommonConstants.IconHub;
            }
        }

        public string SearchButtonGlyph
        {
            get
            {
                return CommonConstants.IconSearch;
            }
        }
    }
}