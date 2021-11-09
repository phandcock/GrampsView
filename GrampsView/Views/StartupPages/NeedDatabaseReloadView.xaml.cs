namespace GrampsView.Views
{
    public sealed partial class NeedDatabaseReloadPage : ViewBase
    {
        public NeedDatabaseReloadPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}