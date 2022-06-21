namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class FamilyListPage : ViewBasePage
    {
        private FamilyListViewModel _viewModel { get; set; }

        public FamilyListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<FamilyListViewModel>();
        }
    }
}