namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class RepositoryListPage : ViewBase
    {
        private RepositoryListViewModel _viewModel { get; set; }

        public RepositoryListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<RepositoryListViewModel>();
        }
    }
}