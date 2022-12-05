namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class CitationListPage : ViewBasePage
    {
        private CitationListViewModel _viewModel { get; set; }

        public CitationListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetRequiredService<CitationListViewModel>();
        }
    }
}