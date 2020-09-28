namespace GrampsView.UserControls
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

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
                return IconFont.Zmdi3dRotation;
            }
        }

        public string SearchButtonGlyph
        {
            get
            {
                return CommonConstants.IconSearch;
            }
        }

        public string StatusText
        {
            get
            {
                // TODO fix this so it changes as the status message changes
                return DataStore.CN.MajorStatusMessage;
            }
        }
    }
}