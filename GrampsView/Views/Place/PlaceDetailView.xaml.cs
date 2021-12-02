namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class PlaceDetailPage : ViewBase
    {
        private PlaceDetailViewModel _viewModel { get; set; }

        public PlaceDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<PlaceDetailViewModel>();
        }
    }
}