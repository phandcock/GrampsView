namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class FamilyDetailPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public FamilyDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}