namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PersonBirthdayPage : ViewBase
    {
        private BookMarkListPage _viewModel { get; set; }

        public PersonBirthdayPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}