namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class PersonDetailPage : ViewBase
    {
        public PersonDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}