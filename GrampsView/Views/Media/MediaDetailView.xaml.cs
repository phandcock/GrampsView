namespace GrampsView.Views
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.ViewModels;

    using System;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public partial class MediaDetailPage : ViewBase
    {
        public MediaDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<ItemsViewModel>();
        }

        private void daMediaElement_MediaFailed(object sender, EventArgs e)
        {
            (sender as Xamarin.CommunityToolkit.UI.Views.MediaElement).Source = null;

            ErrorInfo argDetail = new ErrorInfo
                {
                    { "Type", "Media Element" },
                    { "e", e.ToString() },
                };

            argDetail.Text = "Error displaying Media Element";

            DataStore.Instance.CN.NotifyError(argDetail);

            // TODO Handle when can not play video better
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Frame theFrame = sender as Frame;
            IMediaModel theModel = (theFrame.BindingContext as MediaDetailViewModel).CurrentMediaObject;

            OpenFileRequest t = new OpenFileRequest(theModel.GDescription, new ReadOnlyFile(theModel.MediaStorageFilePath));
            Launcher.OpenAsync(t);
        }
    }
}