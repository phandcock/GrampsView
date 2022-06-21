namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class HubPage : ViewBasePage
    {
        private HubViewModel _viewModel { get; set; }

        public HubPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<HubViewModel>();
        }
    }
}