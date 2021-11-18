namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class EventDetailPage : ViewBase
    {
        public EventDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}