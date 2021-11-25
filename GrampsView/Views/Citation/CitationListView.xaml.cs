namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class CitationListPage : ViewBase
    {
        public CitationListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}