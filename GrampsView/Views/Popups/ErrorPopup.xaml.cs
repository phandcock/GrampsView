namespace GrampsView.Views
{
    using GrampsView.Data.Repository;
    using GrampsView.ViewModels;
using SharedSharp.Errors;

    using Xamarin.CommunityToolkit.UI.Views;
    using Xamarin.Forms;

    public sealed partial class ErrorPopup : Popup
    {
        public ErrorPopup()
        {
            InitializeComponent();

            BindingContext = new ErrorDialogViewModel();

            Size = new Size(SharedSharp.Misc.SharedSharpStatic.ScreenSize.Width - 100, SharedSharp.Misc.SharedSharpStatic.ScreenSize.Height - 100);
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            App.Current.Services.GetService<IErrorNotifications>().DialogShown = false;

            if (App.Current.Services.GetService<IErrorNotifications>().PopupQueue.Count > 0)
            {
                DataStore.Instance.CN.PopUpShow();
            }

            Dismiss(null);
        }
    }
}