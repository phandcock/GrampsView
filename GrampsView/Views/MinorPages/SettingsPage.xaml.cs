namespace GrampsView.Views
{
    using GrampsView.ViewModels.MinorPages;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class SettingsPage : ViewBasePage
    {
        public SettingsPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetRequiredService<SettingsViewModel>();
        }

        private SettingsViewModel _viewModel { get; set; }
    }
}