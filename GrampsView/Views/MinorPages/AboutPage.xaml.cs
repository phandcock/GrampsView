namespace GrampsView.Views
{
    using GrampsView.ViewModels.MinorPages;

    public sealed partial class AboutPage : ViewBasePage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<AboutViewModel>();
        }

        private AboutViewModel _viewModel { get; set; }
    }
}