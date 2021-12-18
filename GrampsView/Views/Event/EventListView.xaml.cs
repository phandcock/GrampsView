namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class EventListPage : ViewBase
    {
        private EventListViewModel _viewModel { get; set; }

        public EventListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = App.Current.Services.GetService<EventListViewModel>();
        }
    }
}