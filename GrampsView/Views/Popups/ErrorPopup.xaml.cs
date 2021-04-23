namespace GrampsView.Views
{
    using GrampsView.Data.Repository;
    using GrampsView.ViewModels;

    using Xamarin.CommunityToolkit.UI.Views;

    public sealed partial class ErrorPopup : Popup
    {
        public ErrorPopup()
        {
            InitializeComponent();

            BindingContext = new ErrorDialogViewModel();

            Size = Common.CardSizes.Current.WindowSize;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            //TODO finish adding ability to display multiple errorpops in a row using the notification queue

            DataStore.Instance.CN.DialogShown = false;

            DataStore.Instance.CN.PopUpShow();

            Dismiss(null);
        }
    }
}