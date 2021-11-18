namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class RepositoryRefDetailPage : ViewBase
    {
        public RepositoryRefDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}