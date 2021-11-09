namespace GrampsView.Views
{
    public sealed partial class WhatsNewPage : ViewBase
    {
        public WhatsNewPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}