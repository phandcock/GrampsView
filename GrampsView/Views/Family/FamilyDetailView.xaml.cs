namespace GrampsView.Views
{
    using GrampsView.ViewModels.Family;

    using Microsoft.Extensions.DependencyInjection;

    public partial class FamilyDetailPage : ViewBasePage
    {
        private FamilyDetailViewModel _viewModel { get; set; }

        public FamilyDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetRequiredService<FamilyDetailViewModel>();
        }
    }
}