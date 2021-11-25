namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class AttributeDetailPage : ViewBase
    {
        public AttributeDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}