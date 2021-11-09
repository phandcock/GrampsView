namespace GrampsView.Views
{
    public sealed partial class TagListPage : ViewBase
    {
        public TagListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}