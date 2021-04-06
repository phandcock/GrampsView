namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Xamarin.CommunityToolkit.UI.Views;

    public sealed partial class ErrorPopup : Popup
    {
        public ErrorPopup()
        {
            InitializeComponent();

            BindingContext = new ErrorDialogViewModel();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Dismiss(null);
        }
    }
}