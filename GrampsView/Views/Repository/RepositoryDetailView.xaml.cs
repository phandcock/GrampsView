namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class RepositoryDetailPage : ViewBase
    {
        private RepositoryDetailViewModel _viewModel { get; set; }

        public RepositoryDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<RepositoryDetailViewModel>();
        }
    }
}