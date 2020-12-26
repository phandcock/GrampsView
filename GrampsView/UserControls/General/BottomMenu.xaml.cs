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

        public string SearchButtonGlyph
        {
            get
            {
                return CommonConstants.IconSearch;
            }
        }

        private async void HubButton_Pressed(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("HubPage");
        }

        private async void SearchButton_Pressed(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("SearchPage");
        }
    }
}