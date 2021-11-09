namespace GrampsView.Views
{
    public partial class EventDetailPage : ViewBase
    {
        public EventDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}