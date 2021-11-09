namespace GrampsView.Views
{
    public sealed partial class AboutPage : ViewBase
    {
        public AboutPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}