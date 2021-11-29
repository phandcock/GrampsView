namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class RepositoryListPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public RepositoryListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}