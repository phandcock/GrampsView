namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class CitationListPage : ViewBase
    {
        private CitationListViewModel _viewModel { get; set; }

        public CitationListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<CitationListViewModel>();
        }
    }
}