namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class SearchPage : ViewBase
    {
        public SearchPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            this.SearchBar.Focus();
        }
    }
}