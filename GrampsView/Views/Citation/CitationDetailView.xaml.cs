namespace GrampsView.Views
{
    using GrampsView.ViewModels.Citation;

    using Microsoft.Extensions.DependencyInjection;

    public partial class CitationDetailPage : ViewBasePage
    {
        private CitationDetailViewModel _viewModel { get; set; }

        public CitationDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<CitationDetailViewModel>();
        }
    }
}