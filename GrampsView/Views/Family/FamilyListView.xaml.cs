namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class FamilyListPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public FamilyListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}