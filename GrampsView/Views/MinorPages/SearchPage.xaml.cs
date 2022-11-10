namespace GrampsView.Views
{
    using GrampsView.ViewModels.MinorPages;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class SearchPage : ViewBasePage
    {
        private SearchPageViewModel _viewModel { get; set; }

        public SearchPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<SearchPageViewModel>();
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            this.SearchBar.Focus();
        }
    }
}