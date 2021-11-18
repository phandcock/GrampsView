namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class SourceDetailPage : ViewBase
    {
        public SourceDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}