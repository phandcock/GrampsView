namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class RepositoryListPage : ViewBasePage
    {
        private RepositoryListViewModel _viewModel { get; set; }

        public RepositoryListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetRequiredService<RepositoryListViewModel>();
        }
    }
}