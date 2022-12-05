namespace GrampsView.Views
{
    using GrampsView.ViewModels.StartupPages;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class FirstRunPage : ViewBasePage
    {
        private FirstRunViewModel _viewModel { get; set; }

        public FirstRunPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<FirstRunViewModel>();
        }
    }
}