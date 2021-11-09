namespace GrampsView.Views
{
    public sealed partial class FirstRunPage : ViewBase
    {
        public FirstRunPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}