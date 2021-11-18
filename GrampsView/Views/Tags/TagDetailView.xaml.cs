namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class TagDetailPage : ViewBase
    {
        public TagDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}