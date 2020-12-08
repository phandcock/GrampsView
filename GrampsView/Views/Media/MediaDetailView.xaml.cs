namespace GrampsView.Views
{
    using GrampsView.Data.Model;
    using GrampsView.ViewModels;

    using System;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public partial class MediaDetailPage : ContentPage
    {
        public MediaDetailPage()
        {
            InitializeComponent();
        }

        private void daMediaElement_MediaFailed(object sender, EventArgs e)
        {
            (sender as MediaElement).Source = null;

            // TODO Handle when can not play video better
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Frame theFrame = sender as Frame;
            MediaModel theModel = (theFrame.BindingContext as MediaDetailViewModel).CurrentMediaObject;

            OpenFileRequest t = new OpenFileRequest(theModel.GDescription, new ReadOnlyFile(theModel.MediaStorageFilePath));
            Launcher.OpenAsync(t);
        }
    }
}