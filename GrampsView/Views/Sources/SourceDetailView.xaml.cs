namespace GrampsView.Views
{
    using GrampsView.ViewModels.Sources;

    using Microsoft.Extensions.DependencyInjection;

    public partial class SourceDetailPage : ViewBasePage
    {
        private SourceDetailViewModel _viewModel { get; set; }

        public SourceDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetRequiredService<SourceDetailViewModel>();
        }
    }
}