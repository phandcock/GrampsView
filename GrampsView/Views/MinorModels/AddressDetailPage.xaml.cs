using GrampsView.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class AddressDetailPage : ViewBasePage
    {
        private AddressDetailViewModel _viewModel { get; set; }

        public AddressDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<AddressDetailViewModel>();
        }
    }
}