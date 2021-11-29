namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class ChildRefDetailPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public ChildRefDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}