namespace GrampsView.Views
{
    public sealed partial class SourceListPage : ViewBase
    {
        public SourceListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}