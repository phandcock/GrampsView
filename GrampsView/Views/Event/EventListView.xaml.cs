namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class EventListPage : ViewBasePage
    {
        private EventListViewModel _viewModel { get; set; }

        public EventListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = Ioc.Default.GetService<EventListViewModel>();
        }
    }
}