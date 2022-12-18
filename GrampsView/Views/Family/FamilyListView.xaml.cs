namespace GrampsView.Views
{
    using GrampsView.ViewModels.Family;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class FamilyListPage : ViewBasePage
    {
        private FamilyListViewModel _viewModel { get; set; }

        public FamilyListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetRequiredService<FamilyListViewModel>();
        }
    }
}