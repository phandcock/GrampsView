namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PersonBirthdayPage : ViewBase
    {
        public PersonBirthdayPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}