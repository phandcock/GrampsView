namespace GrampsView.Views
{
    public sealed partial class SettingsPage : ViewBase
    {
        public SettingsPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}