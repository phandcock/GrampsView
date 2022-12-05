namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class PlaceDetailPage : ViewBasePage
    {
        private PlaceDetailViewModel _viewModel { get; set; }

        public PlaceDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<PlaceDetailViewModel>();
        }
    }
}