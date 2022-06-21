namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class RepositoryRefDetailPage : ViewBasePage
    {
        private RepositoryRefDetailViewModel _viewModel { get; set; }

        public RepositoryRefDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<RepositoryRefDetailViewModel>();
        }
    }
}