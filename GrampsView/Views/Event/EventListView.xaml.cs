namespace GrampsView.Views
{
    public sealed partial class EventListPage : ViewBase
    {
        public EventListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}