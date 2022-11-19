namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class RepositoryDetailPage : ViewBasePage
    {
        private RepositoryDetailViewModel _viewModel { get; set; }

        public RepositoryDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetService<RepositoryDetailViewModel>();
        }
    }
}