namespace GrampsView.Views
{
    using Xamarin.Forms;

    public sealed partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            this.SearchBar.Focus();
        }
    }
}