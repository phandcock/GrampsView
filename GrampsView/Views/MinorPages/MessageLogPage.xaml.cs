namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class MessageLogPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public MessageLogPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}