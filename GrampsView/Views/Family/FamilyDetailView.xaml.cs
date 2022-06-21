namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class FamilyDetailPage : ViewBasePage
    {
        private FamilyDetailViewModel _viewModel { get; set; }

        public FamilyDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<FamilyDetailViewModel>();
        }
    }
}