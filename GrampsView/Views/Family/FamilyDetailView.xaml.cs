namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class FamilyDetailPage : ViewBase
    {
        public FamilyDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}