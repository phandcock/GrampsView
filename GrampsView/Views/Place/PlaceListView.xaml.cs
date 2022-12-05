namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PlaceListPage : ViewBasePage
    {
        private PlaceListViewModel _viewModel { get; set; }

        public PlaceListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = Ioc.Default.GetRequiredService<PlaceListViewModel>();
        }
    }
}