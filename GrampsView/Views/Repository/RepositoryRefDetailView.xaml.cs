namespace GrampsView.Views
{
    public partial class RepositoryRefDetailPage : ViewBase
    {
        public RepositoryRefDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}