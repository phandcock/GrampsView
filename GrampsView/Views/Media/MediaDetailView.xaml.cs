namespace GrampsView.Views
{
    using GrampsView.Data.Model;
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;

    using System;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public partial class MediaDetailPage : ViewBasePage
    {
        public MediaDetailPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = App.Current.Services.GetService<MediaDetailViewModel>();
        }

        private MediaDetailViewModel _viewModel { get; set; }

        private void daMediaElement_MediaFailed(object sender, EventArgs e)
        {
            (sender as Xamarin.CommunityToolkit.UI.Views.MediaElement).Source = null;

            ErrorInfo argDetail = new ErrorInfo
                {
                    { "Type", "Media Element" },
                    { "e", e.ToString() },
                };

            argDetail.ErrorArea = "Error displaying Media Element";

            App.Current.Services.GetService<IErrorNotifications>().NotifyError(argDetail);

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