namespace GrampsView.Views
{
    public partial class RepositoryDetailPage : ViewBase
    {
        public RepositoryDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}