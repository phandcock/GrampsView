namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class AddressDetailPage : ViewBase
    {
        public AddressDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}