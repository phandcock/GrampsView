namespace GrampsView.Views
{
    using Xamarin.Forms;

    /// <summary>
    /// Search Results Page.
    /// </summary>
    public sealed partial class SearchPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchView"/> class.
        /// </summary>
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