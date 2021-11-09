namespace GrampsView.Views
{
    public sealed partial class RepositoryListPage : ViewBase
    {
        public RepositoryListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}