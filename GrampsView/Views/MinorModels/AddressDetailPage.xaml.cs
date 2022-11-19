namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class AddressDetailPage : ViewBasePage
    {
        private AddressDetailViewModel _viewModel { get; set; }

        public AddressDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetService<AddressDetailViewModel>();
        }
    }
}