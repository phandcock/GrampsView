namespace GrampsView.Views
{
    public sealed partial class PersonBirthdayPage : ViewBase
    {
        public PersonBirthdayPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }
    }
}