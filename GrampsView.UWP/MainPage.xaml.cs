namespace GrampsView.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new GrampsView.App(new UWPInit()));
        }
    }
}