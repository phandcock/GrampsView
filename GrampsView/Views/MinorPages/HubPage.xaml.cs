namespace GrampsView.Views
{
    public sealed partial class HubPage : ViewBase
    {
        public HubPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}