namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class AttributeDetailPage : ViewBasePage
    {
        private AttributeDetailViewModel _viewModel { get; set; }

        public AttributeDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<AttributeDetailViewModel>();
        }
    }
}