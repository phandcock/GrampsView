namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class CitationDetailPage : ViewBase
    {
        private CitationDetailViewModel _viewModel { get; set; }

        public CitationDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<CitationDetailViewModel>();
        }
    }
}