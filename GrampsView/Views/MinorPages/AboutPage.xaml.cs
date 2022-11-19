namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class AboutPage : ViewBasePage
    {
        public AboutPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetService<AboutViewModel>();
        }

        private AboutViewModel _viewModel { get; set; }
    }
}