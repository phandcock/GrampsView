namespace GrampsView.Views
{
    public sealed partial class FileInputHandlerPage : ViewBase
    {
        public FileInputHandlerPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}