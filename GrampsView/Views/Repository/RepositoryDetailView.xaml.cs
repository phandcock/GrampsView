namespace GrampsView.Views
{
    using GrampsView.ViewModels.Repository;

    using Microsoft.Extensions.DependencyInjection;

    public partial class RepositoryDetailPage : ViewBasePage
    {
        private RepositoryDetailViewModel _viewModel { get; set; }

        public RepositoryDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetRequiredService<RepositoryDetailViewModel>();
        }
    }
}