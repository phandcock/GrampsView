namespace GrampsView.Views
{
    public sealed partial class BookMarkListPage : ViewBase
    {
        public BookMarkListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}