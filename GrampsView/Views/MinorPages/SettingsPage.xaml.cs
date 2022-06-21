namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class SettingsPage : ViewBasePage
    {
        public SettingsPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<SettingsViewModel>();
        }

        private SettingsViewModel _viewModel { get; set; }
    }
}