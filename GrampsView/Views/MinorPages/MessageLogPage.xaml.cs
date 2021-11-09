namespace GrampsView.Views
{
    public sealed partial class MessageLogPage : ViewBase
    {
        public MessageLogPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}