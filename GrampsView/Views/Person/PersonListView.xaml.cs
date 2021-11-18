namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PersonListPage : ViewBase
    {
        public PersonListPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}