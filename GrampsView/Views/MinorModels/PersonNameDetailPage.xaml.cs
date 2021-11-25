namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class PersonNameDetailPage : ViewBase
    {
        public PersonNameDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}