namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class EventListPage : ViewBase
    {
        private EventListPage _viewModel { get; set; }

        public EventListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = App.Current.Services.GetService<EventListPage>();
        }
    }
}