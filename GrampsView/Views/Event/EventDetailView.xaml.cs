namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class EventDetailPage : ViewBasePage
    {
        private EventDetailViewModel _viewModel { get; set; }

        public EventDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<EventDetailViewModel>();
        }
    }
}