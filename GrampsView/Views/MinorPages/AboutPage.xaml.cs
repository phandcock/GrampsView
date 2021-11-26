namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class AboutPage : ViewBase
    {
        private AboutViewModel _viewModel { get; set; }

        public AboutPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<AboutViewModel>();
        }
    }
}