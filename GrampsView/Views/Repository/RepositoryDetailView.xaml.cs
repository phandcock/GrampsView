namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class RepositoryDetailPage : ViewBase
    {
        public RepositoryDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}