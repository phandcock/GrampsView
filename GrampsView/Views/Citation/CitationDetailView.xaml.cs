namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class CitationDetailPage : ViewBase
    {
        public CitationDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}