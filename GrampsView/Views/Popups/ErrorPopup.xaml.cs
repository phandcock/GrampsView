namespace GrampsView.Views
{
    using GrampsView.Data.Repository;
    using GrampsView.ViewModels;

    using Xamarin.CommunityToolkit.UI.Views;
    using Xamarin.Forms;

    public sealed partial class ErrorPopup : Popup
    {
        public ErrorPopup()
        {
            InitializeComponent();

            BindingContext = new ErrorDialogViewModel();

            Size = new Size(DataStore.Instance.AD.ScreenSize.Width - 100, DataStore.Instance.AD.ScreenSize.Height - 100);
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            DataStore.Instance.CN.DialogShown = false;

            if (DataStore.Instance.CN.PopupQueue.Count > 0)
            {
                DataStore.Instance.CN.PopUpShow();
            }

            Dismiss(null);
        }
    }
}