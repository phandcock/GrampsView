namespace GrampsView.Views
{
    public partial class AddressDetailPage : ViewBase
    {
        public AddressDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}